﻿using Mission_11.Infra;
using System.Text.Json.Serialization;

namespace Mission_11.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart (IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Book b, int quantity)
        {
            base.AddItem(b, quantity);
            Session?.SetJson("Cart", this);
        }

        public override void RemoveLine(Book b)
        {
            base.RemoveLine(b);
            Session?.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cart");
        }
    }
}
