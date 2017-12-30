using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace A2_Project
{
    /// <summary>
    /// Логика взаимодействия для Matrix.xaml
    /// </summary>
    public partial class Matrix : Window
    {
        public Matrix(MainWindow m)
        {
            InitializeComponent();

            int[,] ans = m.AnswerMatrix;

            DataTable dt = new DataTable();

            for (int i = 0; i < ans.GetLength(1); i++)
            {
                dt.Columns.Add(i.ToString());
            }

            //MessageBox.Show(ans.GetLength(0).ToString());
            //MessageBox.Show(ans.GetLength(1).ToString());



            for (int i = 0; i < ans.GetLength(0); i++)
            {
                DataRow NewRows = dt.NewRow();
                dt.Rows.Add(NewRows);

                for (int j = 0; j < ans.GetLength(1); j++)
                {
                    dt.Rows[i].SetField(j, ans[i,j]);
                }

                
            }


            Matrix1.ItemsSource = dt.DefaultView;

        }

        public class clsdatasource
        {
            public int int1 { get; set; }
            public int int2 { get; set; }

            public clsdatasource(int s1, int s2)
            {
                this.int1 = s1;
                this.int2 = s2;
            }
        }
    }
}
