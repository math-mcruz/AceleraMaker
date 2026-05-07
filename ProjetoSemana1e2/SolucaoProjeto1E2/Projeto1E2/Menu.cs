/*
 Menu: Classe principal, que conterá o Método main, responsável por criar o Menu inicial da aplicação com todas as 
 funcionalidades do sistema

 */
using Projeto1E2.Controller;
using Projeto1E2.Contas;
using Projeto1E2.Repository;
using Projeto1E2.Utils;
using Projeto1E2.Exceptions;
class Menu
{
    
    static void Main(string[] args)
    {
        //fazer um imprimir menu principal e menu de operações
        ContaController controller = new ContaController();
        while (true)
        {
            try
            {
                //Exibe menu principal
                byte opcao = ExibirMenu.Principal();
                switch (opcao)
                {
                    case 1:             //cadastrar ------------------------------------------------------>   OK

                        string? novoTitular = ValidacaoHelper.TextoMenu("Digite o nome do titular");

                        int novaAgencia = ValidacaoHelper.ValorPositivo("Digite o número da agência");

                        byte novoTipo = ValidacaoHelper.OpcaoRestricao("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o tipo da Conta", 1, 2);

                        var numeroConta = controller.GerarNumero();

                        float novolimite = 0;

                        int novoAniversario = DateTime.Now.Day;

                        Conta novaConta = null;

                        if (novoTipo == 1)
                        {
                            novaConta = new ContaCorrente(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, limite: novolimite);
                        }
                        else
                        {
                            novaConta = new ContaPoupanca(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, aniversario: novoAniversario);
                        }
                        
                        controller.Cadastrar(novaConta);
                       
                        break;

                    case 2:             //listar todas as contas --------------------------------------------------->   OK

                        controller.ListarTodas();
                        break;

                    case 3:             
                        //da para melhorar atualizando só um tipo de dado ----------------------------------------------------------***************************
                        var contaAtualizar = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja atualizar:", out numeroConta);
                        if (contaAtualizar != null)
                        {
                            controller.Atualizar(contaAtualizar);
                            Console.WriteLine($"Conta atualizada com sucesso, nova conta: {contaAtualizar.GetNumero()}, conta: {numeroConta}, excluida.");
                        }
                        break;

                    case 4:             //deletar conta ----------------------------------------------------------->   OK

                        var contaDeletar = ValidacaoHelper.ExisteCadastro(controller,"Digite o número da conta que deseja atualizar:", out int numeroDeletar);
                        if (contaDeletar != null)
                        {
                            controller.Deletar(numeroDeletar);
                            Console.WriteLine($"Conta deletada com sucesso.");
                        }
                        break;

                    case 5://realizar operações fazer esse sub menu em Utils para não poluir o menu principal
                        bool saida = false;
                        while (!saida)
                        {
                            //Exibe menu de operações
                            byte opcaoOperacao = ExibirMenu.Operacoes();
                            switch (opcaoOperacao)
                            {
                                case 1:             //Sacar ---------------------------------------------------------------------> OK

                                    var contaSaque = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja sacar: ", out int numeroSaque);
                                    if (contaSaque != null)
                                    {
                                        float valorSaque = ValidacaoHelper.ValorPositivo("Digite o valor do depósito: ");
                                        controller.Sacar(numeroSaque, valorSaque);
                                    }
      
                                    break;

                                case 2:         //Depositar ----------------------------------------------------------------------> OK
                                    var contaDeposito = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja depositar: ", out int numeroDeposito);
                                    if (contaDeposito != null)
                                    {
                                        float valorDeposito = ValidacaoHelper.ValorPositivo("Digite o valor do depósito: ");
                                        controller.Depositar(numeroDeposito, valorDeposito);
                                    }
       
                                    break;

                                case 3:         //Transferir ----------------------------------------------------------------------> OK
                                    var contaOrigem = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta origem: ", out int numeroOrigem);
                                    if (contaOrigem != null)
                                    {
                                        var contaDestino = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta destino: ", out int numeroDestino);
                                        if(contaDestino != null)
                                        {
                                            float valorTransferencia = ValidacaoHelper.ValorPositivo("Digite o valor da trasnferência: ");
                                            controller.Transferir(numeroOrigem, numeroDestino, valorTransferencia);
                                        }
                                    }

                                    break;

                                case 4:          //Visualizar Dados ---------------------------------------------------------------> OK

                                    var contaVisualizar = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja visualizar: ", out int numeroVizualizar);
                                    if (contaVisualizar != null)
                                    {
                                        contaVisualizar.Visualizar();
                                    }
                                    break;
                              
                                case 5:          //Sair do menu operações ---------------------------------------------> OK

                                    Console.WriteLine("Voltando para o menu principal");
                                    Console.Clear();
                                    saida = true;
                                    break;

                                default:        //Opção invalida ------------------------------------------------------> OK

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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}