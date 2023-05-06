using Append.Blazor.Sidepanel;
using Domain;
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Components.PackageShipment
{
    public partial class CreateAddress
    {

        [Inject] public ISidepanelService SidePanel { get; set; } = default!;

        // Address info
        private string? Street { get; set; }
        private string? HouseNr { get; set; }
        private string? PostalCode { get; set; }
        private string? City { get; set; }
        private bool Error { get; set; } = false;
        public string ErrorMsg { get; private set; }
        [Parameter] public Action<String> setAddress { get; set; }

        private void submit()
        {
            Error = false;
            if (Street == null || HouseNr == null || PostalCode == null || City == null)
            {
                Error = true;
                ErrorMsg = "Niet alle velden zijn ingevuld";
            }
            else
            {
                setAddress($"{Street} {HouseNr}, {PostalCode} {City}");
                SidePanel.Close();
            }
        }
    }
}