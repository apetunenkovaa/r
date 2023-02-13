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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PageMaterial.xaml
    /// </summary>
    public partial class PageMaterial 
    {
        public static decimal cost;
        public PageMaterial()
        {
            InitializeComponent();
            list.ItemsSource = BDclass.bd.Material.ToList();
            Sortirovka.SelectedIndex = 0;
            List<MaterialType> materials = BDclass.bd.MaterialType.ToList();
            Filtr.Items.Add("Все");
            for (int i = 0; i < materials.Count; i++)
            {
                Filtr.Items.Add(materials[i].Title);
            }
            Filtr.SelectedIndex = 0;
            List<Material> materials2 = BDclass.bd.Material.ToList();

            kolvo.Text = Convert.ToString(materials2.Count) + "/" + Convert.ToString(materials);
        }

        void Filter()
        {
            List<Material> material6 = BDclass.bd.Material.ToList();
            List<Material> material = BDclass.bd.Material.ToList();

            //Поиск
            if (Poisk.Text.Length > 0)
            {
                List<Material> tempList = new List<Material>();

                for (int i = 0; i < material.Count; i++)
                {
                    bool already = true;

                    if (material[i].Title.ToLower().Contains(Poisk.Text.ToLower()))
                    {
                        tempList.Add(material[i]);
                        already = false;
                    }

                    if (material[i].Description != null && material[i].Description.ToLower().Contains(Poisk.Text.ToLower()) && already)
                    {
                        tempList.Add(material[i]);
                    }
                }

                material = tempList;
            }
            //фильтрация
            if (Filtr.SelectedIndex > 0)
            {
                material = material.Where(x => x.MaterialTypeId == Filtr.SelectedIndex).ToList();

            }

            //
            switch (Sortirovka.SelectedIndex)
            {

                case 0:
                    material.Sort((x, y) => x.countinstock.CompareTo(y.CountinStock));
                    break;
                case 1:
                    {
                        material.Sort((x, y) => x.countinstock.CompareTo(y.CountinStock));
                        material.Reverse();
                    }
                    break;
            }
            list.ItemsSource = material;
            if (material.Count > 0)
            {
                kolvo.Text = Convert.ToString(material.Count) + "/" + Convert.ToString(material6.Count);
            }
            else
            {
                MessageBox.Show("Данных нету");
                Poisk.Text = "";
            }

        }


        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filtra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void proiz_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock text = (TextBlock)sender;
            int index = Convert.ToInt32(text.Uid);
            List<MaterialSupplier> materialSuppliers = BDclass.bd.MaterialSupplier.Where(x => x.MaterialID == index).ToList();
            int i = materialSuppliers.Count;
            string suppliers = "";
            if (i == 0)
            {
                text.Text = "Данных нет";

            }
            else
            {
                foreach (MaterialSupplier supplier in materialSuppliers)
                {
                    suppliers += "" + supplier.Supplier.Title + ",";
                }
                text.Text = suppliers.Substring(0, suppliers.Length - 2);
            }
        }
    }
}
