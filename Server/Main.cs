using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Server
{
    

    public partial class Main : Form
    {
        private List<DataGridView> dataGrids = new List<DataGridView>();
        public Main()
        {
            string PathToJsonFolder = "\\\\SERV\\programm";
            string[] pathes = Directory.GetFiles(PathToJsonFolder);

            string PathToJsonFile = null;
            // Установим свойства формы
            this.Text = "SplitContainer Example";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Создание и настройка SplitContainer
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterDistance = 10;

            // Настройка левой панели
            splitContainer.Panel1.BackColor = Color.LightBlue; // Цвет левой панели
            Label label1 = new Label();

            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            ListBox list = new ListBox();
            list.Dock = DockStyle.Fill;
            list.SelectionMode = SelectionMode.One;
            list.ClearSelected();
            foreach (var path in pathes)
            {
                list.Items.Add(path);
            }
            PathToJsonFile = list.SelectedItem as string;
            splitContainer.Panel1.Controls.Add(list);
            splitContainer.Panel2.BackColor = Color.LightBlue; // Цвет правой панели
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            //if (!string.IsNullOrEmpty(PathToJsonFile))
            //{
                
                List<DayLevel> dayLevels = new List<DayLevel>();
                dayLevels = DayLevel.LoadFromFile(pathes[0]);
                for (int i = 0; i < dayLevels.Count; i++) // Добавляем 3 вкладки
                {
                    TabPage tabPage = new TabPage(dayLevels[i].Date);
                    DataGridView dataGridView = new DataGridView();

                    dataGridView.Dock = DockStyle.Fill;
                    dataGridView.DataSource = GetData(dayLevels[i]); // Получаем данные для DataGridView

                    // Добавляем DataGridView на вкладку
                    tabPage.Controls.Add(dataGridView);
                    tabControl.TabPages.Add(tabPage);
                    dataGrids.Add(dataGridView);
                } 
            //}
            splitContainer.Panel2.Controls.Add(tabControl);
            this.Controls.Add(splitContainer);
        }
        private DataTable GetData(DayLevel day)
        {
            DataTable table = new DataTable();
            table.Columns.Add("String Path");
            table.Columns.Add("Size");
            table.Columns.Add("Type");
            table.Columns.Add("Quantity");
            table.Columns.Add("LastWriteTime");
            for (int i = 0; i < (day.dataStorage[0].checkTime).Count(); i++)
            {
                table.Columns.Add(day.dataStorage[0].checkTime[i].ToString());
            }
            for (int i = 0; i < day.dataStorage.Count(); i++)
            {
                string sizes = null;
                string lwrT = null;
                DataRow newRow = table.NewRow();
                for (int j = 0; j < day.dataStorage[i].Size.Count(); j++)
                {
                    sizes += day.dataStorage[i].Size[j].ToString() + ";  \n\r";
                }
                for (int j = 0; j < day.dataStorage[i].lastWriteTime.Count(); j++)
                {
                    lwrT += day.dataStorage[i].lastWriteTime[j].ToString() + ";  \n\r";
                }
                newRow["String Path"] = day.dataStorage[i].Path;
                newRow["Size"] = sizes;
                newRow["Type"] = day.dataStorage[i].type;
                newRow["Quantity"] = day.dataStorage[i].Quantity;
                newRow["LastWriteTime"] = lwrT;
                for (int j = 0; j < (day.dataStorage[0].checkTime).Count(); j++)
                {
                    if ((day.dataStorage[0].checkTime).Count()== (day.dataStorage[0].status).Count())
                    {
                        newRow[day.dataStorage[0].checkTime[j].ToString()] = day.dataStorage[i].status[j].ToString();
                    }
                    
                }
                table.Rows.Add(newRow);
            }
           
            return table;
        }

    }

}
