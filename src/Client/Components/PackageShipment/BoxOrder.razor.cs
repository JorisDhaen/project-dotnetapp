using Append.Blazor.Sidepanel;
using Client.Components.Products;
using Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Orders;
using Shared.Packages;
using Client.Authorisation;

namespace Client.Components.PackageShipment
{
    public partial class BoxOrder
        
    {
         
       
        [Inject] public ISidepanelService SidePanel { get; set; } = default!;
        private OrderDto.Mutate order = new();
        private PackageDto.Mutate package = new();
        private List<OrderItemDto.Mutate> items = new();
        [Inject]
        public IOrderService? OrderService { get; set; }
        [Inject] public NavigationManager? NavigationManager { get; set; }
        [Inject]
        public ShoppingCartState Cart { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Cart.OnChange += StateHasChanged;
        }

        private async Task CreateOrderAsync()
        {
            if(Address == null || Address == "")
            {
                adresIngevuld = false;
                return;
            }
            setOrder();
            setPackage();
            setOrderItems();
            Console.WriteLine(order);
            Console.WriteLine(package);
            Console.WriteLine(items);
            OrderRequest.Create request = new()
            {
                Order = order,
                Package = package,
                Items = items,
            };

            Cart.ClearItems();

            var response = await OrderService.CreateAsync(request);
            NavigationManager.NavigateTo($"orders/{response.OrderId}");
        }

        private void setOrder()
        {
            order.OrderId = Guid.NewGuid().ToString().Substring(0, 6);
            order.NetPrice = PrijsTotaal;
            order.TotalAmount = PrijsTotaal;
            order.OrderDate  = DateTime.Now;
            order.StatusDate = DateTime.Now;         
            order.DeliveryAdress = Address;
            order.InvoiceAdress = Address;
            order.OrderStatus = 1;
            order.UserId = FakeAuthenticationProvider.Current.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private void setPackage()
        {
            package.PackageId = Guid.NewGuid().ToString().Substring(0, 15);
            package.Height = Hoogte;
            package.Width = Breedte;
            package.Dept = Diepte;
            package.Price = (int) PrijsTotaal;
        }

        private void setOrderItems()
        {
            foreach(var cartItem in Cart.Items )
            {
                var orderItem = new OrderItemDto.Mutate()
                {
                    OrderItemId = Guid.NewGuid().ToString().Substring(0, 15),
                    Quantity= cartItem.Quantity,
                    TotalPrice = cartItem.TotalPrice,
                    ProductId = cartItem.Product.ProductId
                };
                items.Add( orderItem );
            }
        }

        private string BoxType { get; set; }
        private string BoxFormat { get; set; }

        private int Breedte { get; set; } = 30;
        private int Hoogte { get; set; } = 50;
        private int Diepte { get; set; } = 50;

        private double PrijsSubTotaal { get; set; }
        private double PrijsTotaal { get; set; }
        private double PrijsDozen { get; set; }
        private double PrijsPaletten { get; set; }
        private double PrijsVerzending { get; set; }

        private int AantalDozen { get; set; }
        private int AantalPaletten { get; set; }
        private bool Error { get; set; } = false;
        public string ErrorMsg { get; private set; }
        private int fee { get; set; } = 0;
        private int totalFee { get; set; } = 0;
        private Pallet pallet = new();
        private List<OrderItem> itemsInCart;
        private TransformProduct productAfmetingen;
        private string? Address { get; set; } = "";
        private bool adresIngevuld { get; set; } = true;

        private void ShowAddressForm()
        {
            SidePanel.Open<CreateAddress>("Adres", "toevoegen", (nameof(setAddress), setAddress));
         
        }

        private void setAddress(String Address)
        {
            this.Address = Address;
        }




        private void formatChange(ChangeEventArgs __e)
        {
            BoxFormat = __e.Value.ToString();
            switch (BoxFormat)
            {
                case "groot":
                    Breedte = 80;
                    Hoogte = 80;
                    Diepte = 80;
                    fee = 1;
                    break;
                case "eigen":
                    Breedte = 50;
                    Hoogte = 30;
                    Diepte = 50;
                    fee = 2;
                    break;
                case "klein":
                    Breedte = 20;
                    Hoogte = 20;
                    Diepte = 20;
                    fee = 1;
                    break;
                default:
                    // code block
                    break;
            }
            BerekenPrijs();
        }
        private void typeChange(ChangeEventArgs __e)
        {
            BoxType = __e.Value.ToString();
            if (BoxType.Equals("standardBox"))
            {
                fee = 0;
                totalFee = 0;
                BerekenPrijs();
            }
            else
            {

            }
        } 

        private void BerekenPrijs()
        {

            Error = false;
            Console.WriteLine("writing cart to a list");
            itemsInCart = Cart.Items.ToList();
            Console.WriteLine("Done writing cart to a list");
            double price = berekenNetPrijs(itemsInCart);
            BerekenPrijsNaSubtotaal(price, itemsInCart);
        }

        private double berekenNetPrijs(List<OrderItem> items)
        {
            return items.Sum(i => i.TotalPrice);
        }

        private void BerekenPrijsNaSubtotaal(double subPrice, List<OrderItem> items)
        {
            try
            {
                /*
          * statische data
          *  */
             
                productAfmetingen = berekenGrootsteAfmetingen(items);

                double prijs_verzending = 15;
                double totalPrice = 0;
                int aantal = berekenAantalProducten(items);
                int breedte = Breedte;
                int hoogte = Hoogte;
                int diepte = Diepte;

                Doos doos = new(breedte, hoogte, diepte);

                int hoeveelheidDozen = BerekenHoeveelDozen(items, doos, productAfmetingen);
                int hoeveelheidPaletten = BerekenHoeveelPaletten( doos, pallet, hoeveelheidDozen);

                double dozenprijs = (hoeveelheidDozen * doos.Prijs);
                double palettenprijs = hoeveelheidPaletten * pallet.Prijs;
                totalFee = hoeveelheidDozen * fee;
                totalPrice += dozenprijs + palettenprijs + (1 * prijs_verzending) + subPrice + totalFee;

                /*
         *  variabel init
         *  */

                AantalPaletten = hoeveelheidPaletten;
                AantalDozen = hoeveelheidDozen;
                PrijsVerzending = prijs_verzending;
                PrijsDozen = dozenprijs;
                PrijsPaletten = palettenprijs;
                PrijsSubTotaal = subPrice;
                PrijsTotaal = totalPrice;

            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMsg = ex.Message;
            }


        }

        private int BerekenHoeveelPaletten(Doos doos, Pallet pallet, double aantalDozen)
        {
            if (doos.Width > pallet.Width || doos.Height > pallet.Height || doos.Depth > pallet.Depth)
            {
                throw new ArgumentException("Deze doos past niet op het pallet");
            }

            double boxVolume = CalculateBoxVolume(doos);
            double palletVolume = pallet.Width * pallet.Height * pallet.Depth;

            double boxesPerPallet = (palletVolume / boxVolume);
            
            double numberOfPallets =  Math.Ceiling( aantalDozen / boxesPerPallet);

            return (int) numberOfPallets;
        }

        private int BerekenHoeveelDozen(List<OrderItem> items,Doos doos, TransformProduct productAfmetingen)
        {
            if (productAfmetingen.Width > doos.Width || productAfmetingen.Height > doos.Height || productAfmetingen.Depth > doos.Depth)
            {
                throw new ArgumentException("De producten passen niet in deze doos");
            }
            double totalVolume = items.Sum(i => i.Product.Width * i.Product.Height * i.Product.Depth * i.Quantity);
            Console.WriteLine($"het totale volume {totalVolume}");

            int numBoxes = (int)Math.Ceiling(totalVolume / CalculateBoxVolume(doos));

            // volume of the products that cannot fit into a full box
            double leftoverSpace = totalVolume % CalculateBoxVolume(doos);

            //there is at least one product that cannot fit into a full box
            if (leftoverSpace > 0)
            {
                double unplacedVolume = 0;
                double currentBoxVolume = 0;
                foreach (var orderItem in items)
                {
                    double itemVolume = orderItem.Product.Width * orderItem.Product.Height * orderItem.Product.Depth;

                    //volume of the item multiplied by its quantity
                    unplacedVolume += itemVolume * orderItem.Quantity;
                    currentBoxVolume += itemVolume * orderItem.Quantity;

                    //current box is full and a new box is needed
                    if (currentBoxVolume > CalculateBoxVolume(doos))
                    {
                        numBoxes++;
                        currentBoxVolume = 0;
                    }
                    //all the products that cannot fit into a full box have been accounted for
                    if (unplacedVolume > leftoverSpace)
                    {
                        break;
                    }
                }
                //at least one product that cannot fit into a full box
                if (currentBoxVolume > 0)
                {
                    numBoxes++;
                }
            }
            Console.WriteLine($"het aantal dozen {numBoxes}");
            return numBoxes;
        }


        private double CalculateBoxVolume(Doos box)
        {
            return box.Width * box.Height * box.Depth;
        }


        private int berekenAantalProducten(List<OrderItem> items)
        {
            return items.Sum(i => i.Quantity);
        }

        private TransformProduct berekenGrootsteAfmetingen(List<OrderItem> items)
        {
            double width = 0;
            double height = 0;
            double depth = 0;
            foreach (OrderItem item in items)
            {
                Product product = item.Product;
                width = Math.Max(width, product.Width);
                height = Math.Max(height, product.Height);
                depth = Math.Max(depth, product.Depth);
            }
            return new TransformProduct((int)width, (int)height, (int)depth);
        }


        public class Pallet
        {
            public int Width { get; } = 120;
            public int Height { get; } = 220;
            public int Depth { get; } = 80;
            public double Prijs { get; } = 10;

            public Pallet()
            {

            }


        }
        public class Doos
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int Depth { get; set; }
            public double Prijs { get; set; } = 1.5;

            public Doos(int width, int height, int depth)
            {
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
              
            }


        }
        public class TransformProduct
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int Depth { get; set; }
       

            public TransformProduct(int width, int height, int depth)
            {
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
         
            }

         
        }
    }
}