﻿using Marketplace.ADOModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Marketplace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MarketplaceEntities Connection = new MarketplaceEntities();

        public static User CurrentUser;
    }
}
