using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Game2
{
    public class XmlManager<T>
    {
        public Type Type { get; private set; }
        
        public T Load(string pPath)
        {
            T instance;
            using (TextReader reader = new StreamReader(pPath))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save(string pPath, object pObj)
        {
            using (TextWriter writer = new StreamWriter(pPath))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, pObj);
            }
        }
    }
}
