

namespace ExternalConfiguration
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Holder { get; set; } = null!;
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"[{Id} {Holder} ({Balance})]";
        }
    }
}
