using Module_11_OOP_WPF_HOME_WORK.BackEnd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Module_11_OOP_WPF_HOME_WORK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseData data = new BaseData();
        Consultant consultant = new Consultant();
        Manager manager = new Manager();
        List<Consultant> conList = new List<Consultant>();
        List<Manager> manList = new List<Manager>();
        bool flag = false;
        string currentId;
        public MainWindow()
        {
            InitializeComponent();
            data.AddFakeClient(100);
        }
        private bool find(Consultant arg)
        {
            return arg.departmentID == (dataGrid.SelectedItem as Department).departmentID;
        }
        private void cbSelected(object sender, SelectionChangedEventArgs e)
        {
            conList = consultant.ConsultantTransformDB(consultant.GetAllClientInDB());
            dataGrid.ItemsSource = conList.Where(find);
        }
        private void dataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {   
            if (flag)
            {
                consultant = dataGrid.SelectedItem as Consultant;
                currentId = consultant.id;
                return;
            }
            manager = dataGrid.SelectedItem as Manager;       
            currentId= manager.id;
        }
        private void btnConsultant(object sender, RoutedEventArgs e)
        {   
            flag = true;

            txtLastName.IsEnabled = false;
            txtName.IsEnabled = false;
            txtSurname.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtPassport.IsEnabled = false;
            txtDepartment.IsEnabled = false;
            btnNewClient.IsEnabled = false;
            btnDelete.IsEnabled = false;
            
            conList = consultant.ConsultantTransformDB(consultant.GetAllClientInDB());
            dataGrid.ItemsSource = conList;
        }
        private void btnManager(object sender, RoutedEventArgs e)
        {  
            flag = false;

            txtLastName.IsEnabled = true;
            txtName.IsEnabled = true;
            txtSurname.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtPassport.IsEnabled = true;
            txtDepartment.IsEnabled = true;
            btnNewClient.IsEnabled = true;
            btnDelete.IsEnabled = true;

            manList = manager.ManagerTransformDB(manager.GetAllClientInDB());
            dataGrid.ItemsSource = manList;
        }
        private void btnNewClient_CLick(object sender, RoutedEventArgs e)
        {
            manager.GreatClient(txtLastName.Text, txtName.Text, txtSurname.Text,
                               txtPhoneNumber.Text, txtPassport.Text,
                               int.Parse(txtDepartment.Text));
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {   
            if (flag)
            {
                conList = consultant.ConsultantTransformDB(consultant.GetAllClientInDB());
                for (int i = 0; i < conList.Count; i++)
                {
                    if (conList[i].id == currentId)
                    {
                        conList[i].phoneNumber = txtPhoneNumber.Text;

                        dataGrid.ItemsSource = conList;

                        consultant.RecordInFile(conList);

                        return;
                    }
                }
            }

            manList = manager.ManagerTransformDB(manager.GetAllClientInDB());
            dataGrid.ItemsSource = manList;

            for (int i = 0; i < manList.Count; i++)
            {
                if (manList[i].id == currentId)
                {
                    manList[i].id = currentId;
                    manList[i].lastName = txtLastName.Text;
                    manList[i].name = txtName.Text;
                    manList[i].surname = txtSurname.Text;
                    manList[i].phoneNumber = txtPhoneNumber.Text;
                    manList[i].seriesPassportNumber = txtPassport.Text;
                    manList[i].departmentID = int.Parse(txtDepartment.Text);

                    dataGrid.ItemsSource = manList;

                    manager.RecordInFile(manList);
                    return;
                }
            }
            

            //manager.RecordInFile(manList);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

