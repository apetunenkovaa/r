using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Material
    {

        public SolidColorBrush solidColor
        {
            get
            {

                if (CountinStock < MinCount)
                {
                    SolidColorBrush color = new SolidColorBrush(Color.FromRgb(221, 210, 90));

                    return color;
                }
                else
                {
                    SolidColorBrush color = new SolidColorBrush(Color.FromRgb(214, 45, 147));
                    return color;
                }
            }
        }

        public double countinstock
        {
            get
            {
                double Countinstock = Convert.ToDouble(CountinStock);
                return Countinstock;
            }
        }
    }
}
