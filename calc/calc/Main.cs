using System;
using System.Windows.Forms;
using System.ComponentModel;
using calc;
using rectanglemethod;
using simpsonmethod;
using trapezoidalmethod;
using calculatemodule;

namespace calc
{
	class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button;

		private System.Windows.Forms.Label m_func_label;
		private System.Windows.Forms.Label m_from_label;
		private System.Windows.Forms.Label m_to_label;
		private System.Windows.Forms.Label m_epsilon_label;

		private System.Windows.Forms.CheckBox m_simpson;
		private System.Windows.Forms.CheckBox m_rectangle;
		private System.Windows.Forms.CheckBox m_trapezoidal;

		private System.Windows.Forms.TextBox m_func_box;
		private System.Windows.Forms.TextBox m_from_box;
		private System.Windows.Forms.TextBox m_to_box;
		private System.Windows.Forms.TextBox m_epsilon_box;

		private System.Windows.Forms.RichTextBox m_result;

		private Calculate m_calculate;
		private RectangleMethod m_rectangle_method;
		private TrapezoidalMethod m_trapezoidal_method;
		private SimpsonMethod m_simpson_method;
		private Container m_object_container;

		public MainForm()
		{
			InitializeComponent();
		}

		void InitializeComponent() {
			this.button = new System.Windows.Forms.Button();

			this.m_func_label = new System.Windows.Forms.Label();
			this.m_from_label = new System.Windows.Forms.Label();
			this.m_to_label = new System.Windows.Forms.Label();
			this.m_epsilon_label = new System.Windows.Forms.Label();

			this.m_simpson = new System.Windows.Forms.CheckBox();
			this.m_rectangle = new System.Windows.Forms.CheckBox();
			this.m_trapezoidal = new System.Windows.Forms.CheckBox();

			this.m_func_box = new System.Windows.Forms.TextBox();
			this.m_from_box = new System.Windows.Forms.TextBox();
			this.m_to_box = new System.Windows.Forms.TextBox();
			this.m_epsilon_box = new System.Windows.Forms.TextBox();

			this.m_result = new System.Windows.Forms.RichTextBox();

			this.SuspendLayout();

			this.button.Location = new System.Drawing.Point(270, 370);
			this.button.Name = "button";
			this.button.Size = new System.Drawing.Size(80, 24);
			this.button.TabIndex = 8;
			this.button.Text = "Посчитать";
			this.button.Click += new EventHandler(MainForm_Click);

			this.m_func_label.Location = new System.Drawing.Point(10, 20);
			this.m_func_label.Name = "m_func_label";
			this.m_func_label.Size = new System.Drawing.Size(40, 20);
			this.m_func_label.Text = "f(x) = ";

			this.m_from_label.Location = new System.Drawing.Point(10, 50);
			this.m_from_label.Name = "m_from_label";
			this.m_from_label.Size = new System.Drawing.Size(40, 20);
			this.m_from_label.Text = "От:";

			this.m_to_label.Location = new System.Drawing.Point(10, 80);
			this.m_to_label.Name = "m_to_label";
			this.m_to_label.Size = new System.Drawing.Size(40, 20);
			this.m_to_label.Text = "До:";
			this.m_to_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

			this.m_epsilon_label.Location = new System.Drawing.Point(10, 110);
			this.m_epsilon_label.Name = "m_epsilon_label";
			this.m_epsilon_label.Size = new System.Drawing.Size(40, 20);
			this.m_epsilon_label.Text = "eps:";
			this.m_epsilon_label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

			this.m_rectangle.Location = new System.Drawing.Point(10, 150);
			this.m_rectangle.Name = "m_rectangle";
			this.m_rectangle.Size = new System.Drawing.Size(280, 24);
			this.m_rectangle.TabIndex = 5;
			this.m_rectangle.Text = "Метод прямоугольников";
			this.m_rectangle.TextAlign = System.Drawing.ContentAlignment.TopRight;

			this.m_trapezoidal.Location = new System.Drawing.Point(10, 180);
			this.m_trapezoidal.Name = "m_trapezoidal";
			this.m_trapezoidal.Size = new System.Drawing.Size(264, 24);
			this.m_trapezoidal.TabIndex = 6;
			this.m_trapezoidal.Text = "Метод трапеций";
			this.m_trapezoidal.TextAlign = System.Drawing.ContentAlignment.TopRight;

			this.m_simpson.Location = new System.Drawing.Point(10, 210);
			this.m_simpson.Name = "m_simpson";
			this.m_simpson.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.m_simpson.Size = new System.Drawing.Size(264, 24);
			this.m_simpson.TabIndex = 7;
			this.m_simpson.Text = "Метод Симпсона";
			this.m_simpson.TextAlign = System.Drawing.ContentAlignment.TopRight;

			this.m_func_box.Location = new System.Drawing.Point(50, 20);
			this.m_func_box.Name = "m_func_box";
			this.m_func_box.Size = new System.Drawing.Size(300, 24);
			this.m_func_box.TabIndex = 1;
			this.m_func_box.Text = "(x^2 * ln(1/x))/(1 + x)";

			this.m_from_box.Location = new System.Drawing.Point(50, 50);
			this.m_from_box.Name = "m_from_box";
			this.m_from_box.Size = new System.Drawing.Size(300, 24);
			this.m_from_box.TabIndex = 2;
			this.m_from_box.Text = "0,1";

			this.m_to_box.Location = new System.Drawing.Point(50, 80);
			this.m_to_box.Name = "m_to_box";
			this.m_to_box.Size = new System.Drawing.Size(300, 24);
			this.m_to_box.TabIndex = 3;
			this.m_to_box.Text = "1,0";

			this.m_epsilon_box.Location = new System.Drawing.Point(50, 110);
			this.m_epsilon_box.Name = "m_epsilon_box";
			this.m_epsilon_box.Size = new System.Drawing.Size(300, 24);
			this.m_epsilon_box.TabIndex = 4;
			this.m_epsilon_box.Text = "0,00001";

			this.m_result.Size = new System.Drawing.Size(340, 120);
			this.m_result.Location = new System.Drawing.Point(10, 240);

			m_calculate = new Calculate();
			m_rectangle_method = new RectangleMethod();
			m_trapezoidal_method = new TrapezoidalMethod();
			m_simpson_method = new SimpsonMethod();

			m_object_container = new Container();
			m_object_container.Add(m_rectangle_method, "rectangle");
			m_object_container.Add(m_trapezoidal_method, "trapezoidal");
			m_object_container.Add(m_simpson_method, "simpson");
			m_object_container.Add(m_calculate, "calculate");

			this.ClientSize = new System.Drawing.Size(360, 400);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
				this.button,
				this.m_epsilon_label,
				this.m_from_label,
				this.m_func_label,
				this.m_to_label,
				this.m_simpson,
				this.m_rectangle,
				this.m_trapezoidal,
				this.m_func_box,
				this.m_from_box,
				this.m_to_box,
				this.m_epsilon_box,
				this.m_result
			});

			this.Text = "Численное интегрирование";
			this.ResumeLayout(false);

			this.Closed += new EventHandler(MainForm_Close);
		}

		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Click(object sender, EventArgs e) {
			try {
				m_calculate.setFunction(m_func_box.Text, "x");

				double _from = Convert.ToDouble(m_from_box.Text);
				double _to = Convert.ToDouble(m_to_box.Text);
				double _epsilon = Convert.ToDouble(m_epsilon_box.Text);

				m_rectangle_method.setValues(_from, _to, _epsilon, m_calculate.calculateAtPoint);
				m_trapezoidal_method.setValues(_from, _to, _epsilon, m_calculate.calculateAtPoint);
				m_simpson_method.setValues(_from, _to, _epsilon, m_calculate.calculateAtPoint);

				m_result.Text = "";

				if (m_rectangle.Checked) {
					m_result.Text =  "Rectangle:   " + m_rectangle_method.integrate().ToString() + "\n";
				}

				if (m_trapezoidal.Checked) {
					m_result.Text += "Trapezoidal: " + m_trapezoidal_method.integrate().ToString() + "\n";
				}

				if (m_simpson.Checked) {
					m_result.Text += "Simpson:     " + m_simpson_method.integrate().ToString();
				}
			}
			catch (ArgumentException ex) {
				m_result.Text = ex.Message;
			}
		}

		private void MainForm_Close (object sender, EventArgs e)
		{
			m_object_container.Dispose();
		}
	}	
}