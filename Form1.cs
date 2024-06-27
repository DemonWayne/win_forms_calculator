using System.Numerics;
using System.Text.RegularExpressions;

namespace l_16_t_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void add_symbol(string sym)
        {
            bottom_val.Text = string.Concat(bottom_val.Text, sym);
        }

        private void dot()
        {
            if (!bottom_val.Text.Contains(",")) add_symbol(",");
        }

        private void add_number(string num)
        {
            if (bottom_val.Text == "0") bottom_val.Text = num;
            else bottom_val.Text = bottom_val.Text += num;
        }

        private void delete_symbol()
        {
            if (bottom_val.Text.Length > 1) bottom_val.Text = bottom_val.Text.Remove(bottom_val.Text.Length - 1);
            else bottom_val.Text = "0";
        }

        private void pre_op(string op)
        {
            if (Regex.IsMatch(top_val.Text, @"$(?<=(\+|\-|\*|\/|\%))"))
            {
                top_val.Text = $"{top_val.Text.Substring(0, top_val.Text.Length - 1)}{op}";
            }
            else
            {
                top_val.Text = string.Concat(bottom_val.Text, $" {op}");
                bottom_val.Text = "0";
            }
        }

        private void calc()
        {
            string[] first_str = top_val.Text.Split(" ");

            float first_num, second_num;
            float.TryParse(first_str[0], out first_num);
            float.TryParse(bottom_val.Text, out second_num);

            float res = 0;
            switch (first_str[1])
            {
                case "+":
                    res = first_num + second_num;
                    break;
                case "-":
                    res = first_num - second_num;
                    break;
                case "*":
                    res = first_num * second_num;
                    break;
                case "/":
                    if (second_num == 0) MessageBox.Show("Ділення на 0 неможливо виконати.");
                    else res = first_num / second_num;
                    break;
                case "%":
                    res = (first_num * second_num) / 100;
                    break;
            }

            top_val.Text = "";
            bottom_val.Text = $"{res}";
        }

        private void one_Click(object sender, EventArgs e) => add_number("1");

        private void two_Click(object sender, EventArgs e) => add_number("2");

        private void three_Click(object sender, EventArgs e) => add_number("3");

        private void four_Click(object sender, EventArgs e) => add_number("4");

        private void five_Click(object sender, EventArgs e) => add_number("5");

        private void six_Click(object sender, EventArgs e) => add_number("6");

        private void seven_Click(object sender, EventArgs e) => add_number("7");

        private void eight_Click(object sender, EventArgs e) => add_number("8");

        private void nine_Click(object sender, EventArgs e) => add_number("9");

        private void zero_Click(object sender, EventArgs e) => add_number("0");

        private void point_Click(object sender, EventArgs e) => dot();

        private void delete_Click(object sender, EventArgs e) => delete_symbol();

        private void plus_Click(object sender, EventArgs e) => pre_op("+");

        private void minus_Click(object sender, EventArgs e) => pre_op("-");

        private void multiply_Click(object sender, EventArgs e) => pre_op("*");

        private void divide_Click(object sender, EventArgs e) => pre_op("/");

        private void percentage_Click(object sender, EventArgs e) => pre_op("%");

        private void equal_Click(object sender, EventArgs e) => calc();

        private void clear_bottom_Click(object sender, EventArgs e) => bottom_val.Text = "0";

        private void change_abs_Click(object sender, EventArgs e)
        {
            if (bottom_val.Text.StartsWith("-")) bottom_val.Text = bottom_val.Text.Substring(1);
            else bottom_val.Text = $"-{bottom_val.Text}";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            top_val.Text = "";
            bottom_val.Text = "0";
        }
        private void square_root_Click(object sender, EventArgs e)
        {
            double num, res;
            num = Convert.ToDouble(bottom_val.Text);
            res = Math.Sqrt(num);
            bottom_val.Text = $"{res}";
        }

        private void square_Click(object sender, EventArgs e)
        {
            double num, res;
            num = Convert.ToDouble(bottom_val.Text);
            res = Math.Pow(num, 2);
            bottom_val.Text = $"{res}";
        }

        private void inverse_Click(object sender, EventArgs e)
        {
            double num, res;
            num = Convert.ToDouble(bottom_val.Text);
            res = 1 / num;
            bottom_val.Text = $"{res}";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int num;
            int.TryParse(e.KeyChar.ToString(), out num);

            if ((num >= 1 && num <= 9) || e.KeyChar == '0') add_number(e.KeyChar.ToString());
            else if (Regex.IsMatch(e.KeyChar.ToString(), @"$(?<=(\+|\-|\*|\/|\%))")) pre_op(e.KeyChar.ToString());
            else if (e.KeyChar == '=' || e.KeyChar == '\r') calc();
            else if (e.KeyChar == ',') dot();
            else if (e.KeyChar == (char)Keys.Back) delete_symbol();
        }
    }
}
