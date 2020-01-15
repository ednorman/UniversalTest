using System;

namespace GRM_Dev_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //The deliberate mistake is "Usages" instead of usage?
            //Or is it the 25st?
            ProductService productService = new ProductService();
            var result = productService.DoWork(args[0]);
            Console.WriteLine(result);
        }
    }
}
