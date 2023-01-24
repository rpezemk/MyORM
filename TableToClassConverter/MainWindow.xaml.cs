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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TableToClassConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }



        private void ConvertSQL2CSharpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SQLTableTextBox.Text == null)
                return;

            var inputSQL = SQLTableTextBox.Text;

            Dictionary<string, string> valuePairs = new Dictionary<string, string>();

            foreach (var line in inputSQL.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (words.Count >= 2)
                {
                    var varName = words[0].Trim('[', ']');
                    var type = "";
                    var potType = words[1];
                    if (potType.StartsWith('['))
                    {
                        type = potType.Split('[', ']', StringSplitOptions.RemoveEmptyEntries)[0].Trim('[', ']');
                        type = type.Split(']', '[', StringSplitOptions.RemoveEmptyEntries)[0].Trim('[', ']');
                    }
                    else
                        type = potType;

                    valuePairs.Add(varName, type);
                }
            }

            var res = string.Join(' ', valuePairs.Select(p => $"    public Field<{Logic.FromSqlType(p.Value)}> {p.Key} " + " { get; set; } \n"));
            CSharpClassTextBox.Text = res;

        }

        
        private void AutoGenerateFromInformationSchema_Click(object sender, RoutedEventArgs e)
        {
            var schemaInfos = Logic.GetInformationSchema(ServerTxb.Text, BazaTxB.Text, LoginTxb.Text, PwdTxb.Text, OwnerTxb.Text);
            SQLTableDG.ItemsSource = schemaInfos;

            ResultingClassesTxtBox.Text = Logic.GetCSharpClasses(schemaInfos);
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultingClassesTxtBox.Text);
            MessageBox.Show("skopiowano");
        }
    }
}
