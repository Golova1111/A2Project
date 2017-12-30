using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        MainWindow mw;
        public Setting(MainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            InsertCoinTB.Text = mw.InsertCoin.ToString();
            ReplaceCoinTB.Text = mw.ReplaceCoin.ToString();
            TransferCoinTB.Text = mw.TransferCoin.ToString();

            DontCheckRegisr.IsChecked = mw.DontCheckRegisr;

        }

        private void CloseButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(InsertCoinTB.Text) > 0 &&
              Convert.ToInt32(ReplaceCoinTB.Text) > 0 &&
              Convert.ToInt32(TransferCoinTB.Text) > 0)
                {

                    mw.InsertCoin = Convert.ToInt32(InsertCoinTB.Text);
                    mw.DeleteCoin = Convert.ToInt32(InsertCoinTB.Text);
                    mw.ReplaceCoin = Convert.ToInt32(ReplaceCoinTB.Text);
                    mw.TransferCoin = Convert.ToInt32(TransferCoinTB.Text);

                    mw.DontCheckRegisr = Convert.ToBoolean(DontCheckRegisr.IsChecked);

                    MessageBox.Show("Изменения успешно сохранены");
                    this.Close();
                }

                else
                    MessageBox.Show("Данные введены некорректно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
