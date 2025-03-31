using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6
{
    public partial class DetailEditForm: Form
    {

        public OrderDetail CurrentDetail { get; } = new OrderDetail();
        public DetailEditForm()
        {
            try
            {
                InitializeComponent();
                if (CurrentDetail.Product == null)
                {
                   CurrentDetail.Product = new Product();
                }
                CurrentDetail = new OrderDetail
                {
                    Product = new Product(),
                    Quantity = 1
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show($"初始化失败: {ex.Message}");
                throw;
            }

            // 绑定数据
            txtProductId.DataBindings.Add("Text", CurrentDetail.Product, "ProductId");
            txtProductName.DataBindings.Add("Text", CurrentDetail.Product, "Name");
            txtPrice.DataBindings.Add("Text", CurrentDetail.Product, "Price");
            numQuantity.DataBindings.Add("Value", CurrentDetail, "Quantity");
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("商品信息不完整", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("请输入有效的价格", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("数量必须大于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK; // 必须设置！
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void DetailEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
