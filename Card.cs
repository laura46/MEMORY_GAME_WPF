using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryProject
{
    class Card
    {
        private ImageSource front, back;
        private bool visible, correct;
        public bool clicked;
        public EventHandler OnCorrectPair;
        //private Image image;

        public Card()
        {
            back = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
            clicked = false;
            visible = true;
            correct = false;
            //front = frontOfCard.Source;
            //image = frontOfCard;
            
        }

        //public Card(ImageSource frontOfCard)
        //{
        //    back = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
        //    clicked = false;
        //    visible = true;
        //    correct = false;
        //    front = frontOfCard;

        //}

        public void Clicked()
        {
            //if (clicked == true)
            //{
            //    clicked = false;
            //    return;
            //}else

            clicked = true;
        }

        public ImageSource Show()
        {
            if (visible || correct)
            {
                if (clicked)
                {
                    if (correct)
                    {
                        OnCorrectPair?.Invoke(this, EventArgs.Empty);
                        return front;

                    }
                    else
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
            if(correct == true)
            {
                clicked = true;
                visible = true;
                Show();
            }else
            clicked = false;
        }

        public void MakeInvisible()
        {
            visible = false;
        }

        public void Correct()
        {
            correct = true;
            visible = true;
        }
    }
}
