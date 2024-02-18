using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_OOP_WPF_HOME_WORK.BackEnd
{
    class Manager : Consultant
    {
        public new string id { get; set; }
        public new string lastName { get; set; }
        public new string name { get; set; }
        public new string surname { get; set; }
        public new string phoneNumber { get; set; }
        public new string seriesPassportNumber { get; set; }
        public List<Manager> manList { get; private set; }
        public new int departmentID { get; set; }
        public new Manager this[int index]
        {
            get { return this.manList[index]; }
        }
        public Manager(string id, string lastName, string name, string surname,
                       string phoneNumber, string seriesPassportNumber, int departmentID)
        {
            this.id = id;
            this.lastName = lastName;
            this.name = name;
            this.surname = surname;
            this.phoneNumber = phoneNumber;
            this.seriesPassportNumber = seriesPassportNumber;
            this.departmentID = departmentID;
        }
        public Manager() : this("", "", "", "", "", "", 0)
        {

        }
        public List<Manager> ManagerTransformDB(List<string> allClientDB)
        {
            int index = 0;
            List<Manager> consList = new List<Manager>();

            foreach (string item in allClientDB)
            {
                Manager cons = new Manager();
                string[] tempMassive = new string[item.Split('|').Length];
                tempMassive = item.Split('|');

                cons.id = tempMassive[0];
                cons.lastName = tempMassive[1];
                cons.name = tempMassive[2];
                cons.surname = tempMassive[3];
                cons.phoneNumber = tempMassive[4];
                cons.seriesPassportNumber = tempMassive[5];
                cons.departmentID = int.Parse(tempMassive[6]);

                consList.Add(cons);

                index++;
            }
            return consList;
        }
        public void GreatClient(string lastName, string name, string surname, string phoneNumber, string passport, int department)
        {
            string id = new BaseData().GetCurrentIdInDB();
            using (StreamWriter sw = File.AppendText(GetPath()))
            {
                sw.Write($"\n{id}|{lastName}|{name}|{surname}|{phoneNumber}|{passport}|{department}|");
            }
        }
        public override string ToString()
        {
            return $"{id} {lastName} {name} {surname} {phoneNumber} {seriesPassportNumber} {departmentID}";
        }

        public void RecordInFile(List<Manager> allClient)
        {
            BaseData baseData = new BaseData();
            using (StreamWriter sw = new StreamWriter(baseData.GetPath()))
            {
                for (int i = 0; i < allClient.Count; i++)
                {
                    sw.WriteLine($"{allClient[i].id}|" +
                                $"{allClient[i].lastName}|" +
                                $"{allClient[i].name}|" +
                                $"{allClient[i].surname}|" +
                                $"{allClient[i].phoneNumber}|" +
                                $"{allClient[i].passportNumber}|" +
                                $"{allClient[i].departmentID}|");
                }
            }
        }
    }
}
