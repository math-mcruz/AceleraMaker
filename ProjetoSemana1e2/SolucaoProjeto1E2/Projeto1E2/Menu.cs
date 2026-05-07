/*
 Menu: Classe principal, que conterá o Método main, responsável por criar o Menu inicial da aplicação com todas as 
 funcionalidades do sistema

 Cores: Classe utilitária, que possui a função de aplicar cores ao Menu
 */
using Projeto1E2.Controller;
using Projeto1E2.Contas;
using Projeto1E2.Repository;
using Projeto1E2.Utils;
class Menu
{
    
    static void Main(string[] args)
    {
        //fazer um imprimir menu principal e menu de operações
        ContaController controller = new ContaController();
        while (true)
        {
            
            Console.WriteLine("    Sistema Bancário");
            Console.WriteLine("\n\tMenu:\n");
            Console.WriteLine("[1] - Cadastrar");
            Console.WriteLine("[2] - Listar todas as contas");
            Console.WriteLine("[3] - Atualizar conta");
            Console.WriteLine("[4] - Deletar conta");
            //posso fazer um segundo menu para gerenciar as operações
            Console.WriteLine("[5] - Realizar operações");
            Console.WriteLine("[6] - Sair\n");
            //byte pois o numero é muito pequeno e economiza memória
            byte opcao = Convert.ToByte(Console.ReadLine());

            switch (opcao)
            {
                case 1://cadastrar
                    Console.WriteLine("Digite o nome do titular");
                    string? novoTitular = Console.ReadLine();
                    
                    Console.WriteLine("\nDigite o número da agência");
                    var novaAgencia = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o tipo da Conta");
                    //tratamento de erro para o tipo da conta

                    byte novoTipo = Convert.ToByte(Console.ReadLine());
                    
                    var numeroConta = controller.GerarNumero();
                    
                    float novolimite = 0;
                    
                    int novoAniversario = DateTime.Now.Day;
                    
                    Conta novaConta = null;
                    
                    if (novoTipo == 1)
                    {
                        novaConta = new ContaCorrente( numero : numeroConta, agencia : novaAgencia, tipo : novoTipo, titular: novoTitular, limite: novolimite);
                    }
                    else
                    {
                        novaConta = new ContaPoupanca( numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, aniversario: novoAniversario);
                    }
                    controller.Cadastrar(novaConta);
                    Console.WriteLine($"Conta cadastrada com sucesso, numero da conta é: {numeroConta}");
                    break;

                case 2://listar todas as contas
                    controller.ListarTodas();
                    break;

                case 3://atualizar conta
                    Console.WriteLine("Digite o número da conta que deseja atualizar:");
                    int numeroContaAtualizar = Convert.ToInt32(Console.ReadLine());
                    if (controller.BuscarNaCollection(numeroContaAtualizar) != null)
                    {
                        controller.Atualizar(controller.BuscarNaCollection(numeroContaAtualizar));
                        Console.WriteLine($"Conta atualizada com sucesso, nova conta: {controller.BuscarNaCollection(numeroContaAtualizar).GetNumero()}, conta: {numeroContaAtualizar} foi excluida ");
                    }
                    else
                    {
                        Console.WriteLine("Conta não encontrada");
                    }
                    break;

                case 4://deletar conta
                    Console.WriteLine("Digite o número da conta que deseja deletar:");
                    int numeroContaDeletar = Convert.ToInt32(Console.ReadLine());
                    if (controller.BuscarNaCollection(numeroContaDeletar) != null)
                    {
                        controller.Deletar(numeroContaDeletar);
                    }
                    else
                    {
                        Console.WriteLine("Conta não encontrada");
                    }
                    break;

                case 5://realizar operações fazer esse sub menu em Utils para não poluir o menu principal
                    bool saida = false;
                    while (!saida)
                    {
                        Console.Clear(); 
                        Console.WriteLine("\tOPERAÇÕES BANCÁRIAS\n");
                        Console.WriteLine("[1] - Sacar");
                        Console.WriteLine("[2] - Depositar");
                        Console.WriteLine("[3] - Transferir");
                        Console.WriteLine("[4] - Visualizar dados da conta");
                        Console.WriteLine("[5] - Voltar para o menu principal");
                        Console.WriteLine("\nDigite a operação desejada: ");
                        byte subOpcao = Convert.ToByte(Console.ReadLine());
                        switch (subOpcao)
                        {
                            case 1: //sacar
                                Console.WriteLine("Digite o número da conta:");
                                int numeroSaque = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Digite o valor do saque:");
                                float valorSaque = Convert.ToSingle(Console.ReadLine());

                                controller.Sacar(numeroSaque, valorSaque);
                                Console.WriteLine($"Saque realizado com sucesso, saldo atual: {controller.BuscarNaCollection(numeroSaque).GetSaldo()}");
                                break;
                            
                            case 2:
                                Console.WriteLine("Digite o número da conta:");
                                int numeroDeposito = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Digite o valor do depósito:");
                                float valorDeposito = Convert.ToSingle(Console.ReadLine());

                                controller.Depositar(numeroDeposito, valorDeposito);
                                Console.WriteLine($"Depósito realizado com sucesso, saldo atual: {controller.BuscarNaCollection(numeroDeposito).GetSaldo()}");
                                break;
                            
                            case 3:
                                Console.WriteLine("Digite o número da conta de origem:");
                                int numeroContaOrigem = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Digite o número da conta de destino:");
                                int numeroContaDestino = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Digite o valor da transferência:");
                                float valorTransferencia = Convert.ToSingle(Console.ReadLine());

                                controller.Transferir(numeroContaOrigem, numeroContaDestino, valorTransferencia);
                                Console.WriteLine($"Transferência realizada com sucesso, saldo atual da conta de origem: {controller.BuscarNaCollection(numeroContaOrigem).GetSaldo()}, saldo atual da conta de destino: {controller.BuscarNaCollection(numeroContaDestino).GetSaldo()}");
                                break;

                            case 4:
                                Console.WriteLine("Digite o número da conta:");
                                int numeroContaVisualizar = Convert.ToInt32(Console.ReadLine());
                                var contaVisualizar = controller.BuscarNaCollection(numeroContaVisualizar);
                                if(contaVisualizar != null)
                                {
                                    contaVisualizar.Visualizar();
                                }
                                else
                                {
                                    Console.WriteLine($"Conta {numeroContaVisualizar} não encontrada.");
                                }
                                break;
                            
                            case 5:
                                Console.WriteLine("Voltando para o menu principal");
                                Console.Clear();
                                saida = true;
                                break;

                            default:
                                Console.WriteLine("Opção inválida, digite outra.");
                                break;

                        }
                        Console.WriteLine("\nAperte qualquer tecla para continuar.");
                        Console.ReadKey();
                        Console.Clear();

                    }
                    break;

                case 6://sair
                    Console.WriteLine("Encerrando o menu");
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite outra.");
                    break;
            }
            Console.WriteLine("\nAperte qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}