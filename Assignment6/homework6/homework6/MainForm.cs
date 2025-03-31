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
    public partial class MainForm: Form
    {
        private OrderService orderService = new OrderService();
        private BindingSource orderBindingSource = new BindingSource();
        private BindingSource detailBindingSource = new BindingSource();
        public MainForm()
        {
            InitializeComponent();
            // 初始化数据绑定
            orderBindingSource.DataSource = orderService.GetAllOrders();
            detailBindingSource.DataSource = orderBindingSource;
            detailBindingSource.DataMember = "Details";

            // 绑定订单列表
            orderDataGridView.DataSource = orderBindingSource;

            // 绑定订单明细列表
            detailDataGridView.DataSource = detailBindingSource;

            // 设置列显示
            ConfigureDataGridViews();

            // 添加一些测试数据
            AddTestData();
        }
        private void ConfigureDataGridViews()
        {
            // 订单列表配置
            orderDataGridView.AutoGenerateColumns = false;
            orderDataGridView.Columns.Clear();
            orderDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderId",
                HeaderText = "订单号"
            });
            orderDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Customer.Name",
                HeaderText = "客户姓名"
            });
            orderDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderDate",
                HeaderText = "下单日期"
            });
            orderDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "总金额"
            });

            // 订单明细列表配置
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
        private void AddTestData()
        {
            var product1 = new Product { ProductId = "P001", Name = "笔记本电脑", Price = 5999 };
            var product2 = new Product { ProductId = "P002", Name = "鼠标", Price = 99 };
            var product3 = new Product { ProductId = "P003", Name = "键盘", Price = 199 };

            var customer1 = new Customer { CustomerId = "C001", Name = "张三", ContactInfo = "13800138000" };
            var customer2 = new Customer { CustomerId = "C002", Name = "李四", ContactInfo = "13900139000" };

            var order1 = new Order
            {
                OrderId = "O001",
                Customer = customer1,
                OrderDate = DateTime.Now,
                Details = new List<OrderDetail>
                {
                    new OrderDetail { Product = product1, Quantity = 1 },
                    new OrderDetail { Product = product2, Quantity = 2 }
                }
            };

            var order2 = new Order
            {
                OrderId = "O002",
                Customer = customer2,
                OrderDate = DateTime.Now.AddDays(-1),
                Details = new List<OrderDetail>
                {
                    new OrderDetail { Product = product2, Quantity = 1 },
                    new OrderDetail { Product = product3, Quantity = 3 }
                }
            };

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            orderBindingSource.DataSource = orderService.GetAllOrders();
            orderBindingSource.ResetBindings(false);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new OrderEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    orderService.AddOrder(editForm.CurrentOrder);
                    orderBindingSource.DataSource = orderService.GetAllOrders();
                    orderBindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加订单失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择要删除的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedOrder = orderDataGridView.CurrentRow.DataBoundItem as Order;
            if (selectedOrder == null) return;

            if (MessageBox.Show($"确定要删除订单 {selectedOrder.OrderId} 吗?", "确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    orderService.RemoveOrder(selectedOrder.OrderId);
                    orderBindingSource.DataSource = orderService.GetAllOrders();
                    orderBindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除订单失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择要修改的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedOrder = orderDataGridView.CurrentRow.DataBoundItem as Order;
            if (selectedOrder == null) return;

            var editForm = new OrderEditForm(selectedOrder);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    orderService.UpdateOrder(editForm.CurrentOrder);
                    orderBindingSource.DataSource = orderService.GetAllOrders();
                    orderBindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"修改订单失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (var queryForm = new QueryForm())
            {
                if (queryForm.ShowDialog() == DialogResult.OK)
                {
                    IEnumerable<Order> orders = null;
                    var allOrders = orderService.GetAllOrders(); // 获取所有订单

                    switch (queryForm.SelectedQueryType)
                    {
                        case QueryForm.QueryType.ByOrderId:
                            orders = allOrders.Where(o => o.OrderId.Contains(queryForm.Keyword));
                            break;
                        case QueryForm.QueryType.ByCustomer:
                            orders = allOrders.Where(o => o.Customer.Name.Contains(queryForm.Keyword));
                            break;
                        case QueryForm.QueryType.ByProduct:
                            // 如果需要按商品名称查询，需要检查订单明细
                            orders = allOrders.Where(o =>
                                o.Details.Any(d => d.Product.Name.Contains(queryForm.Keyword)));
                            break;
                        case QueryForm.QueryType.ByAmountRange:
                            orders = allOrders.Where(o =>
                                o.TotalAmount >= queryForm.MinAmount &&
                                o.TotalAmount <= queryForm.MaxAmount);
                            break;
                    }

                    // 更新数据显示
                    orderBindingSource.DataSource = orders?.ToList() ?? new List<Order>();
                    orderBindingSource.ResetBindings(false);
                }
            }
        }
        [STAThread]
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "文本文件|*.txt|所有文件|*.*";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        orderService.ExportToFile(saveDialog.FileName);
                        MessageBox.Show("订单导出成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 重置数据源
                var orderService = new OrderService();
                orderDataGridView.DataSource = orderService.GetAllOrders(); // 重新加载数据


                // 3. 可选：显示提示
                MessageBox.Show("已重置订单列表", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"重置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public enum QueryType
    {
        ByOrderId,
        ByCustomer,
        ByProduct,
        ByAmountRange
    }

        private void orderDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
