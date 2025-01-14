﻿using Microsoft.AspNetCore.Mvc;
using Mission_11.Models;

namespace Mission_11.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;


        public CartSummaryViewComponent(Cart cartservice)
        {
            cart = cartservice;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
