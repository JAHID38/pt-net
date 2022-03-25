namespace pt_net.Manager.PtNet
{
    public interface IPtNetService
    {
        public int GenerateNumeric();
        public string GenerateAlphanumeric();
        public double GenerateFloat();
    }
}