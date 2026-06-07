using System;
using System.IO;
using System.Security.Cryptography;



partial class  Program

{
    static decimal balance = 0;
    static string Credentials = "Credentials.txt";
    static string Historial = "Historial.txt";
    static string Finanzas = "Finanzas.txt";
    
  static void Main()
    {
        
       
        
        
        if (!File.Exists(Historial))
        {
            File.Create(Historial).Close();
        }
        if (!File.Exists(Finanzas))
        {
            File.Create(Finanzas).Close();
        } 
         
        
  
        
        if (!File.Exists(Credentials))
        {
            Console.WriteLine("Bienvenido, por favor ingrese su nombre de usuario:");
            string? Nombre = Console.ReadLine();
            Console.WriteLine("Un gusto conocerte! " + Nombre + ", por favor ingresa tu contraseña:");
            string clave = Console.ReadLine() ?? string.Empty;
            string claveCifrada = EncryptPassword(clave);
            Console.WriteLine("Gracias por registrarte, " + Nombre + "!");
            using (StreamWriter sw = File.CreateText(Credentials))
            {
                sw.WriteLine(Nombre);
                sw.WriteLine(claveCifrada);
            }

        
            sign();


        }

        else if (File.Exists(Credentials) && File.ReadAllText(Credentials).Trim() == "")
        {
            File.Delete(Credentials);
            Console.WriteLine("Los archivos estan corrompidos o vacios, por favor, vuelva a crear su cuenta");
            Main();
            return;

        }

        else
        {
            using (StreamReader sr = File.OpenText(Credentials))
            {
                string? Nombre = sr.ReadLine();
                string? claveCifrada = sr.ReadLine();
                Console.WriteLine("Bienvenido,"+  Nombre  + "Porfavor ingresa tu contraseña:" );
                string? inputClave = Console.ReadLine();
                if (EncryptPassword(inputClave ?? string.Empty) == claveCifrada)
                {
                 
                   sign();
                 }
                else
                {
                    while (EncryptPassword(inputClave ?? string.Empty) != claveCifrada)
                    {
                        Console.WriteLine("Contraseña incorrecta, por favor intente de nuevo.");
                        inputClave = Console.ReadLine();
                        if (EncryptPassword(inputClave ?? string.Empty) == claveCifrada)
                        {
                          
                           sign();
                        }
                    }
            
                }
            }
                        
        }


       void finanza()
        {

            while  (true)
        {
            Console.WriteLine("¿Qué operación deseas realizar? (ingresa el número correspondiente) \n1. registrar ingreso \n2. registrar Gasto \n3. mostrar balance \n4. ver historial  \n5. salir");
            string? opcion = Console.ReadLine();
           
            switch (opcion)
            {
                
                case "1":
                    Console.WriteLine("Ingrese el monto del ingreso:");
                     string? ingreso = Console.ReadLine();
                     if (decimal.TryParse(ingreso, out decimal ingresoDecimal))
                    {
                        
                        using (StreamWriter sw = File.AppendText(Finanzas ))
                        {
                            balance += ingresoDecimal;
                            sw.WriteLine( balance);
                        }
                        using (StreamWriter sw = File.AppendText(Historial))
                        {
                            sw.WriteLine("Ingreso: " + ingresoDecimal + " - " + DateTime.Now);
                        }

                        
                    }
                    else
                    {
                        Console.WriteLine("Monto no válido, por favor intente de nuevo.");
                    }                     
                    break;
                case "2":
                    Console.WriteLine("Ingrese el monto del gasto:");
                     string? gasto = Console.ReadLine();
                    if (decimal.TryParse(gasto, out decimal gastoDecimal))
                    {
                         
                        using (StreamWriter sw = File.AppendText(Finanzas))
                        {
                           balance -= gastoDecimal;
                          sw.WriteLine(balance);
                        }
                        using (StreamWriter sw = File.AppendText(Historial))
                        {
                            sw.WriteLine("Gasto: " + gastoDecimal + " - " + DateTime.Now);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Monto no válido, por favor intente de nuevo.");
                    }
                    break;
                case "3":
                    if (File.Exists(Finanzas) && new FileInfo(Finanzas).Length > 0)

                        {
                            string b = File.ReadLines(Finanzas).Last();
                            Console.WriteLine( "Balance actual: " + b);
                            
                        }
                        else
                        {
                            Console.WriteLine("Balance actual: 0");
                        }
               
                            
                                
                        
                    break;
                case "4":
                    // Ver historial
                    Console.WriteLine("Historial de transacciones:");
                    using (StreamReader srw = File.OpenText(Historial))
                    {
                        string? linea;
                        while ((linea = srw.ReadLine()) != null)
                        {
                            Console.WriteLine(linea);
                        }
                    }
                    break;
                case "5":
                   Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }

            
        }
        void sign()
        {
            int vueltas = 0;
            while (vueltas < 3)
            {
                Console.WriteLine("\r iniciando sesion...");
                Thread.Sleep(1000);
                Console.WriteLine("\r iniciando sesion..");
                Thread.Sleep(1000);
                Console.WriteLine("\r iniciando sesion.");
                Thread.Sleep(1000);
                Console.WriteLine("\r iniciando sesion..");
                vueltas++;


            }

        if (vueltas == 3)
         {
             Console.WriteLine("Sesion iniciada con exito!");
             Thread.Sleep(1000);
        if (File.ReadAllText(Finanzas).Trim() != "")
    {
    
        string ultimaLinea = File.ReadLines(Finanzas).Last().Trim();

        if (decimal.TryParse(ultimaLinea, out balance))
        {
            Console.WriteLine("Cargado con exito ");
        }
        else
        {
            Console.WriteLine("null exeption, no se pudo cargar el balance, iniciando con balance en 0.");
            balance = 0;
        }

        finanza();
    }
        else
        {
      
         balance = 0;
         finanza();
        }  
    }
    

        }
        
    }

    static string EncryptPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return string.Empty;
        }

        using (var sha256 = SHA256.Create())
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

}



