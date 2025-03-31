using System;
using System.Windows.Forms;

namespace homework6
{
    public partial class QueryForm : Form
    {
        // 查询类型枚举
        public enum QueryType
        {
            ByOrderId,    // 按订单号
            ByCustomer,   // 按客户
            ByProduct,    // 按商品
            ByAmountRange // 按金额范围
        }

        // 公开属性：主窗体可通过这些属性获取用户输入
        public QueryType SelectedQueryType { get; private set; }
        public string Keyword { get; private set; }
        public decimal MinAmount { get; private set; }
        public decimal MaxAmount { get; private set; }

        public QueryForm()
        {
            InitializeComponent();
            rbOrderId.Checked = true; // 默认选中“按订单号查询”
            UpdateQueryUI();         // 初始化界面状态
        }

        // 根据选择的查询类型更新控件状态
        private void UpdateQueryUI()
        {
            // 只有选择“金额范围”时才禁用关键字输入框，启用金额输入框
            txtKeyword.Enabled = !rbAmount.Checked;
            numMinAmount.Enabled = rbAmount.Checked;
            numMaxAmount.Enabled = rbAmount.Checked;

            // 切换查询类型时清空无关字段
            if (rbAmount.Checked)
                txtKeyword.Text = string.Empty;
            else
            {
                numMinAmount.Value = 0;
                numMaxAmount.Value = 0;
            }
        }

        // 单选按钮事件统一处理
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                UpdateQueryUI();
        }

        // 点击“确定”按钮
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!rbAmount.Checked && string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                MessageBox.Show("请输入查询关键字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbAmount.Checked)
            {
                if (numMinAmount.Value > numMaxAmount.Value)
                {
                    MessageBox.Show("最小金额不能大于最大金额", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (numMinAmount.Value < 0 || numMaxAmount.Value < 0)
                {
                    MessageBox.Show("金额不能为负数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 保存查询参数
            if (rbOrderId.Checked)
            {
                SelectedQueryType = QueryType.ByOrderId;
                Keyword = txtKeyword.Text.Trim();
            }
            else if (rbCustomer.Checked)
            {
                SelectedQueryType = QueryType.ByCustomer;
                Keyword = txtKeyword.Text.Trim();
            }
            else if (rbProduct.Checked)
            {
                SelectedQueryType = QueryType.ByProduct;
                Keyword = txtKeyword.Text.Trim();
            }
            else if (rbAmount.Checked)
            {
                SelectedQueryType = QueryType.ByAmountRange;
                MinAmount = numMinAmount.Value;
                MaxAmount = numMaxAmount.Value;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // 点击“取消”按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}