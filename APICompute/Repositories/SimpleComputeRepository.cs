namespace APICompute.Repositories
{
    public class SimpleComputeRepository
    {
        public async Task<string> HelloWorldAsync(string your_name)
        {
            return await Task.Run(() => $"Hello {your_name}!!!");
        }

        public async Task<double> CelciusToFahrenheitAsync(double c)
        {
            return await Task.Run(() =>
            {
                double f = c*1.8+32;
                return f;
            });
        }
    }
}
