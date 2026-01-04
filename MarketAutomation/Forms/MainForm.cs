using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketAutomation.Models;

namespace MarketAutomation
{
    public partial class MainForm : Form
    {
        // Ürün listesi
        List<Product> products = new List<Product>();

        // Sepet
        List<Product> cart = new List<Product>();
        decimal totalPrice = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        // FORM LOAD
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Örnek ürünler
            products.Add(new Product { Id = 1, Name = "Elma", Price = 10, Stock = 50 });
            products.Add(new Product { Id = 2, Name = "Ekmek", Price = 8, Stock = 30 });
            products.Add(new Product { Id = 3, Name = "Süt", Price = 15, Stock = 20 });

            RefreshProductList();
            lblTotal.Text = "Toplam: 0 ₺";
        }

        // Ürünleri ListBox'a bas
        private void RefreshProductList()
        {
            lstProducts.Items.Clear();

            foreach (var product in products)
            {
                lstProducts.Items.Add(product);
            }
        }

        // ÜRÜN EKLE
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ürün adı boş olamaz!");
                return;
            }

            if (!decimal.TryParse(nudProductPrice.Text, out decimal price))
            {
                MessageBox.Show("Fiyat geçersiz!");
                return;
            }

            if (!int.TryParse(nudProductStock.Text, out int stock))
            {
                MessageBox.Show("Stok geçersiz!");
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Fiyat 0'dan büyük olmalı!");
                return;
            }

            if (stock < 0)
            {
                MessageBox.Show("Stok negatif olamaz!");
                return;
            }

            Product newProduct = new Product
            {
                Id = products.Count + 1,
                Name = name,
                Price = price,
                Stock = stock
            };

            products.Add(newProduct);
            RefreshProductList();

            // Alanları temizle
            txtProductName.Clear();
            nudProductPrice.Clear();
            nudProductStock.Clear();
        }

        // SEPETE EKLE
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçin");
                return;
            }

            Product selectedProduct = (Product)lstProducts.SelectedItem;

            if (selectedProduct.Stock <= 0)
            {
                MessageBox.Show("Bu ürün stokta yok!");
                return;
            }

            selectedProduct.Stock--;
            cart.Add(selectedProduct);

            lstCart.Items.Add($"{selectedProduct.Name} - {selectedProduct.Price:C}");

            totalPrice += selectedProduct.Price;
            lblTotal.Text = "Toplam: " + totalPrice.ToString("C");

            RefreshProductList();
        }

        // SATIŞI TAMAMLA
        private void btnSell_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Sepet boş!");
                return;
            }

            MessageBox.Show(
                $"Satış tamamlandı!\nToplam Tutar: {totalPrice:C}",
                "Satış",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            cart.Clear();
            lstCart.Items.Clear();
            totalPrice = 0;
            lblTotal.Text = "Toplam: 0 ₺";
        }
    }
}
