using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GRM_Dev_Test
{
    public sealed class PartnerProductService : ProductService
    {
        private PartnerProductService()
        {
        }

        public static PartnerProductService Instance { get; } = new PartnerProductService();

        public List<string> Contracts { get; set; }
        public List<DistributionPartnerContract> Partners { get; set; }
        public string Header { get; set; }

        public override string Validate(string args)
        {
            var index = args.IndexOf(" ");
            var partner = args.Substring(0, index);
            var date = ParseToDatetime(args.Substring(index + 1));
            var usage = GetUsageFromPartnerName(partner);
            var validList = ValidateList(Contracts, date, usage);

            List<string> cleanList = new List<string>();
            foreach (var line in validList)
            {
                cleanList.Add(RemoveUsage(line, usage));
            }
            cleanList.Sort();
            var returnstring = $" {Header}{Environment.NewLine}";
            foreach (var str in cleanList)
            {
                returnstring += $" {str}{Environment.NewLine}";
            }
            return returnstring;
        }

        private string GetUsageFromPartnerName(string partnername)
        {
            var partner = Partners.Find(p => p.Partner == partnername);
            return partner.Usage;
        }

        private string RemoveUsage(string line, string usageToKeep)
        {
            var datapoints = line.Split("|");
            if(datapoints[2].Contains(usageToKeep))
            {
                datapoints[2] = usageToKeep;
            }
            var rejoinedstring = String.Join("|", datapoints);
            return rejoinedstring;
        }


        public override void LoadData()
        {
            Partners = new List<DistributionPartnerContract>();
            using (StreamReader reader = new StreamReader("TextFileInput2.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("Partner"))
                    {
                        continue;
                    }
                    var data = line.Split("|");

                    Partners.Add(new DistributionPartnerContract
                    {
                        Partner = data[0],
                        Usage = data[1]
                    });
                }
            }

            Contracts = new List<string>();
            using (StreamReader reader = new StreamReader("TextFileInput1.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.Contains("|") )
                    {
                        continue;
                    }
                    if(line.Contains("Artist"))
                    {
                        line = line.Remove(18, 1);
                        Header = line;
                    }
                    else
                    {
                        Contracts.Add(line);
                    }
                }
            }
        }

        public List<string> ValidateList(List<string> list, DateTime date, string usage)
        {
            //return a list where the start dates are before the date
            //and the end dates are after the date
            List<string> validatedList = new List<string>();
            foreach (var item in list)
            {
                if(!item.Contains(usage))
                {
                    continue;
                }
                var result = CheckDates(item, date);
                if (result != null)
                {
                    validatedList.Add(result);
                }
            }
            return validatedList;
        }

        private string CheckDates(string line, DateTime date)
        {
            var datapoints = line.Split("|");

            DateTime startDate = ParseToDatetime(datapoints[3]);
            DateTime endDate = ParseToDatetime(datapoints[4]);

            if(date > startDate && date < endDate)
            {
                return line;
            }
            else
            {
                return null;
            }
        }

        private DateTime ParseToDatetime(string datestring)
        {
            //if no end date set to max
            if (String.IsNullOrEmpty(datestring))
            {
                return DateTime.MaxValue;
            }

            //trim month to 3 chars
            var splitdate = datestring.Split(" ");
            var month = splitdate[1];
            var trimmedMonth = month.Substring(0, 3);
            splitdate[1] = trimmedMonth;
            datestring = $"{splitdate[0]} {splitdate[1]} {splitdate[2]}";
            
            int index = char.IsNumber(datestring, 1) ? 2 : 1;
            //remove suffix
            datestring = datestring.Substring(0, index) + datestring.Substring(index + 2);
            var result = DateTime.ParseExact(datestring, "d MMM yyyy", CultureInfo.InvariantCulture);

            return result;
        }
    }
}
