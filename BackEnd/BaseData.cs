using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Module_11_OOP_WPF_HOME_WORK.BackEnd
{
    class BaseData
    {
        public string GetPath()
        {
            return "C:\\Users\\User\\source\\repos\\Module_11_OOP_WPF_HOME_WORK\\BackEnd\\DB\\database.csv";
        }
        public void AddFakeClient(int countClients)
        {
            using (StreamWriter sw = new StreamWriter(GetPath()))
            {
                for (int i = 1; i < countClients; i++)
                {
                    var faker = new Faker();
                    Random rand = new Random();

                    sw.WriteLine($"{i}|" +
                                $"{faker.Person.LastName}|" +
                                $"{faker.Person.FirstName}|" +
                                $"{faker.Person.UserName}|" +
                                $"{faker.Phone.PhoneNumber()}|" +
                                $"{rand.Next(100_000_000, 9_000_000_00)}_{rand.Next(10000, 90000)}|" +
                                $"{rand.Next(1, 6)}|");

                }
            }
        }
        public List<string> GetAllClientInDB()
        {
            List<string> allClients = new List<string>();

            using (StreamReader sr = File.OpenText(GetPath()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {

                    allClients.Add(s);
                }
            }
            return allClients;
        }
        public string GetOneClientInDB(string passport)
        {
            List<string> allClient = GetAllClientInDB();
            string errorMassage = "Not found";

            for (int i = 0; i < allClient.Count; i++)
            {
                string[] mc = allClient[i].Split('|');

                if (mc.Contains(passport))
                {
                    return allClient[i];
                }
            }
            return errorMassage;
        }
        public string GetCurrentIdInDB()
        {
            List<string> allClient = GetAllClientInDB();

            HashSet<int> idHash = new HashSet<int>();

            int idCurrent = allClient.Count;

            foreach (string client in allClient)
            {
                string[] tempCl = client.Split('|');

                idHash.Add(int.Parse(tempCl[0]));
            }
            while (true)
            {
                idCurrent++;

                if (!idHash.Contains(idCurrent))
                {
                    return idCurrent.ToString();
                }
                continue;
            }
        }
    }
}
