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
    public partial class OrderEditForm: Form
    {
        public Order CurrentOrder { get; set; }
        private BindingSource detailBindingSource = new BindingSource();
        public OrderEditForm()
        {
            InitializeComponent();
            CurrentOrder = new Order
            {
                OrderId = GenerateOrderId(),
                OrderDate = DateTime.Now,
                Details = new List<OrderDetail>()
            };
            InitializeControls();
        }
        public OrderEditForm(Order order) : this()
        {
            CurrentOrder = order;
            InitializeControls();
        }
         private void InitializeControls()
        {
            // 先清除所有现有绑定
            txtOrderId.DataBindings.Clear();
            dtpOrderDate.DataBindings.Clear();
            txtCustomerId.DataBindings.Clear();
            txtCustomerName.DataBindings.Clear();
            txtContactInfo.DataBindings.Clear();

            // 绑定订单基本信息
            txtOrderId.DataBindings.Add("Text", CurrentOrder, "OrderId");
            dtpOrderDate.DataBindings.Add("Value", CurrentOrder, "OrderDate");
            
            // 绑定客户信息
            if (CurrentOrder.Customer == null)
                CurrentOrder.Customer = new Customer();
            
            txtCustomerId.DataBindings.Add("Text", CurrentOrder.Customer, "CustomerId");
            txtCustomerName.DataBindings.Add("Text", CurrentOrder.Customer, "Name");
            txtContactInfo.DataBindings.Add("Text", CurrentOrder.Customer, "ContactInfo");
            
            // 绑定订单明细
            detailBindingSource.DataSource = CurrentOrder.Details;
            detailDataGridView.DataSource = detailBindingSource;
            
            // 配置明细列表
            detailDataGridView.AutoGenerateColumns = false;
            detailDataGridView.Columns.Clear();
            detailDataGridView.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "Product.Name", 
                HeaderText = "商品名称" 
            });
            detailDataGridView.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "Product.Price", 
                HeaderText = "单价" 
            });
            detailDataGridView.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "Quantity", 
                HeaderText = "数量" 
            });
            detailDataGridView.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TotalAmount", 
                HeaderText = "小计" 
            });
        }
        
        private string GenerateOrderId()
        {
            return "O" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void BtnAddDetail_Click(object sender, EventArgs e)
        {
            using (var detailForm = new DetailEditForm())
            {
                if (detailForm.ShowDialog() == DialogResult.OK)
                {
                    // 确保商品信息有效
                    if (detailForm.CurrentDetail.Product == null)
                    {
                        detailForm.CurrentDetail.Product = new Product
                        {
                            Name = "未命名商品",
                            Price = 0
                        };
                    }

                    // 添加到当前订单
                    CurrentOrder.Details.Add(detailForm.CurrentDetail);

                    // 自动刷新表格
                    detailBindingSource.ResetBindings(false);
                }
            }
        }
        private void BtnRemoveDetail_Click(object sender, EventArgs e)
        {
            if (detailDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择要删除的明细");
                return;
            }

            var selectedDetail = detailDataGridView.CurrentRow.DataBoundItem as OrderDetail;
            if (selectedDetail != null)
            {
                // 从集合移除
                CurrentOrder.Details.Remove(selectedDetail);

                // 自动刷新表格
                detailBindingSource.ResetBindings(false);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderId.Text))
            {
                MessageBox.Show("订单号不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("客户信息不完整", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (CurrentOrder.Details.Count == 0)
            {
                MessageBox.Show("请至少添加一个订单明细", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void OrderEditForm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
