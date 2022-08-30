using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms_Task7.Views;

namespace WindowsForms_Task7.Presenters
{
    public class MainPresenter
    {
        public decimal priceOfHotDog = 2.30M;
        public decimal priceOfHamburger = 2.50M;
        public decimal priceOfFries = 1.00M;
        public decimal priceOfCocaCola = 0.80M;

        public decimal priceOfAI92 = 1.20M; // per liter
        public decimal priceOfAI95 = 1.60M; // per liter
        public decimal priceOfAI98 = 1.90M; // per liter

        public IMainView _view;

        public MainPresenter(IMainView view)
        {
            _view = view;

            _view.OilSelecetedIndexChanged += oilsComboBox_SelectedIndexChanged_1;
            _view.ByMoneyRBChanged += byMoneyRb_CheckedChanged_1;
            _view.MoneyTxtBKeyPress += moneyTxtb_KeyPress_1;
            _view.MoneyTxtBTextChanged += moneyTxtb_TextChanged_1;
        }

        private void CalculateOil()
        {
            string oil = _view.SelectedOil;
            if (oil != null)
            {
                dynamic total = 0;
                var oilPrice = 0M;

                if (oil == "AI92")
                {
                    oilPrice = priceOfAI92;
                }
                else if (oil == "AI95")
                {
                    oilPrice = priceOfAI95;
                }
                else if (oil == "AI98")
                {
                    oilPrice = priceOfAI98;
                }

                if (_view.ByMoney)
                {
                    if (!string.IsNullOrEmpty(_view.Money))
                    {
                        var result = decimal.Parse(_view.Money);
                        _view.OilTotal = result + ".00";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_view.Liter))
                    {
                        int liters = int.Parse(_view.Liter);
                        _view.OilTotal = ((decimal)liters * oilPrice).ToString();
                    }
                }
            }
        }

        private void oilsComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string oil = _view.SelectedOil;
            if (oil == "AI92")
            {
                _view.PriceOfOil = priceOfAI92.ToString();
            }
            if (oil == "AI95")
            {
                _view.PriceOfOil = priceOfAI95.ToString();
            }
            if (oil == "AI98")
            {
                _view.PriceOfOil = priceOfAI98.ToString();
            }
            _view.OilWarning = string.Empty;
            CalculateOil();
        }

        private void byMoneyRb_CheckedChanged_1(object sender, EventArgs e)
        {
            _view.OilTotal = 0.ToString();

            if (_view.ByMoney)
            {
                _view.EnableMoneyText = true;
                _view.EnableLiterText= false;
                _view.Liter = String.Empty;
            }
            else
            {
                _view.EnableLiterText = true;
                _view.EnableMoneyText = false;
                _view.Money = String.Empty;
            }

            CalculateOil();
        }

        private void moneyTxtb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int index = tb.SelectionStart + tb.SelectionLength;
            if (e.KeyChar == '\b' && tb.Text != String.Empty && index != 0)
            {
                tb.Text = tb.Text.Remove(index - 1, 1);
                tb.SelectionStart = index - 1;
            }
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void moneyTxtb_TextChanged_1(object sender, EventArgs e)
        {
            if (_view.SelectedOil == null) // if nothing selected
            {
                _view.OilWarning = "Choose Oil";
            }
            else
            {
                _view.OilWarning = string.Empty;
            }

            CalculateOil();
        }
    }
}
