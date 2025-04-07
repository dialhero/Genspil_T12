namespace Genspil
{
    internal class Customer
    {
        private string _name;
        private string _email;
        private int _phoneNumber;
        public List<Request> requestList = new List<Request>();


        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public int PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public Customer() //Parameterløs constructor for at kunne oprette new Customer uden parametre
        {
            _name = "ukendt";
            _email = "ukendt";
            _phoneNumber = 00000000;
            requestList = new List<Request>();
        }

        public Customer(string name, string email, int phoneNumber)
        {
            _name = name ?? "ukendt";
            _email = email ?? "ukendt";
            _phoneNumber = phoneNumber > 0 ? phoneNumber : 00000000;
            requestList = new List<Request>();
        }

        public static void ShowCustomers() //Til at teste add metoden og CSV filen
        {
            Console.WriteLine("\nListe over kunder:");
            foreach (var customer in Program.customerList)
            {
                Console.WriteLine($"Navn: {customer.Name}, Email: {customer.Email}, Tlf: {customer.PhoneNumber}");
            }
        }

        public static Customer GetCustomerByPhoneNumber(int phoneNumber)
        {
            foreach (var customer in Program.customerList)
            {
                if (customer._phoneNumber == phoneNumber)
                {
                    return customer;
                }
            }
            return null;
        }

        public static string getContactDetails(List<Customer> customers, int phoneNumber)

        {
            Customer customer = customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

            if (customer != null)
            {
                return $"Navn: {customer.Name}, Email: {customer.Email}, Telefonnummer: {customer.PhoneNumber}";
            }
            else
            {
                return "Brugeren er ikke fundet.";
            }

        }
        public static Customer ParseCustomerData(string customerData)
        {
            string[] parts = customerData.Split(',');

            if (parts.Length < 2)
            {
                throw new FormatException("Fejl i kundendata.");
            }

            string name = parts[0].Trim();
            string email = parts[1].Trim();
            string number = parts[2].Trim();
            int phoneNumber = int.Parse(number.Split(' ')[0]);
            return new Customer(name, email, phoneNumber);
        }
        
        //Metode brugt til debugging
        public static void printcustomer()
        {
            foreach (var customer in Program.customerList)
            {
                Console.WriteLine(customer.Name);
            }
        }
    }
}






