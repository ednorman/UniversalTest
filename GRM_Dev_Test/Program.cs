using System;

namespace GRM_Dev_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //The deliberate mistake is the 25st
            ProductService productService = PartnerProductService.Instance;
            var result = productService.TemplateMethod(args[0]);
            Console.WriteLine(result);
        }
    }
}
