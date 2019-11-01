using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryProject
{
    class Card
    {
        private ImageSource front, back;
        public bool clicked;
        private bool visible;

        public Card(ImageSource frontOfCard)
        {
            back = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
            clicked = false;
            visible = true;
            front = frontOfCard;
        }

        public void Clicked()
        {
            if (clicked == true)
            {
                clicked = false;
                return;
            }else 

            clicked = true;
        }

        public ImageSource Show()
        {
            if (visible)
            {
                if (clicked)
                {
                    return front;
                }
                else
                {
                    return back;
                }
            }
            else
                return null;
        }

        public void FlipToBack()
        {
            clicked = false;
        }

        public void MakeInvisible()
        {
            visible = false;
        }
    }
}
