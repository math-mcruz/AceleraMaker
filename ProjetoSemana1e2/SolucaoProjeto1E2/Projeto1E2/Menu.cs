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
        ContaController controller = new ContaController();
        while (true)
        {
            try
            {
                //Exibe menu principal
                Cores.CorMenu();
                byte opcao = ExibirMenu.Principal();
                Cores.CorOriginal();
                switch (opcao)
                {
                    case 1:             //cadastrar - Validações(ValidacaoHelper) para garantir os dados certos

                        string? novoTitular = ValidacaoHelper.TextoMenu("\nDigite o nome do titular:\n");

                        int novaAgencia = ValidacaoHelper.ValorPositivo("\nDigite o número da agência:\n");

                        byte novoTipo = ValidacaoHelper.OpcaoRestricao("\n[1] - Corrente\t[2] - Poupança\n\nDigite qual o tipo da Conta\n", 1, 2);

                        var numeroConta = controller.GerarNumero();

                        float novoLimite = 0;
                        
                        int novoAniversario = DateTime.Today.Day;//gera só o dia, sem mês e ano

                        Conta novaConta = null;

                        if (novoTipo == 1) //verifica o tipo para instanciar corretamente, tipo 1 é conta corrente
                        {
                            novoLimite = ValidacaoHelper.ValorPositivo("\nDigite o número do Limite:\n");
                            novaConta = new ContaCorrente(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, limite: novoLimite);
                        }
                        else //como tem a validação só pode 1 e 2, e 2 é conta poupança
                        {
                            novaConta = new ContaPoupanca(numero: numeroConta, agencia: novaAgencia, tipo: novoTipo, titular: novoTitular, aniversario: novoAniversario);
                        }
                        
                        controller.Cadastrar(novaConta);
                        break;

                    case 2:             //listar todas as contas 

                        controller.ListarTodas();
                        break;

                    case 3:             //Atualizar conta, Validações(ValidacaoHelper) para garantir os dados certos

                        //precisa ter a conta para poder atualizar
                        var contaAntiga = ValidacaoHelper.ExisteCadastro(controller, "\nDigite o número da conta que deseja atualizar:\n", out numeroConta);
                        if (contaAntiga != null)
                        { 
                            controller.Atualizar(contaAntiga);

                            Cores.ExibirSucesso("Conta atualizada com sucesso");
                            
                        }
                        break;

                    case 4:             //deletar conta, Validações(ValidacaoHelper) para garantir os dados certos

                        //precisa ter a conta para poder deletar

                        var contaDeletar = ValidacaoHelper.ExisteCadastro(controller,"Digite o número da conta que deseja deletar:", out int numeroDeletar);
                        if (contaDeletar != null)
                        {
                            controller.Deletar(numeroDeletar);
                            Cores.ExibirSucesso("Conta deletada com sucesso.");
                        }
                        break;

                    case 5:            //Procurar por número, Validações(ValidacaoHelper) para garantir os dados certos

                        //precisa ter a conta para poder mostrar
                        int numeroProcurar = ValidacaoHelper.ValorPositivo("\nDigite o número que deseja procurar: \n");
                        controller.ProcurarPorNumero(numeroProcurar);
                        break;

                    case 6:         //realizar operações, sub menu em Utils para não poluir o menu principal

                        bool saidaOperacoes = false;
                        while (!saidaOperacoes)
                        {
                            Console.Clear();
                            //Exibe menu de operações
                            Cores.CorMenu();
                            byte opcaoOperacao = ExibirMenu.Operacoes();
                            Cores.CorOriginal();
                            switch (opcaoOperacao)
                            {
                                case 1:             //Sacar, Validações(ValidacaoHelper) para garantir os dados certos

                                    //precisa ter a conta para poder sacar

                                    var contaSaque = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja sacar: ", out int numeroSaque);
                                    if (contaSaque != null)
                                    {
                                        float valorSaque = ValidacaoHelper.ValorPositivo("Digite o valor do saque: ");
                                        controller.Sacar(numeroSaque, valorSaque);
                                    }
      
                                    break;

                                case 2:         //Depositar, Validações(ValidacaoHelper) para garantir os dados certos

                                    //precisa ter a conta para poder depositar
                                    var contaDeposito = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja depositar: ", out int numeroDeposito);
                                    if (contaDeposito != null)
                                    {
                                        float valorDeposito = ValidacaoHelper.ValorPositivo("Digite o valor do depósito: ");
                                        controller.Depositar(numeroDeposito, valorDeposito);
                                    }
       
                                    break;

                                case 3:         //Transferir, Validações(ValidacaoHelper) para garantir os dados certos

                                    //precisa ter as contas para poder transferir entre elas
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

                                case 4:          //Visualizar dados, Validações(ValidacaoHelper) para garantir os dados certos

                                    //precisa ter a conta para poder mostrar
                                    var contaVisualizar = ValidacaoHelper.ExisteCadastro(controller, "Digite o número da conta que deseja visualizar: ", out int numeroVizualizar);
                                    if (contaVisualizar != null)
                                    {
                                        contaVisualizar.Visualizar();
                                    }
                                    break;
                              
                                case 5:          //Sair do menu operações 

                                    Console.WriteLine("Voltando para o menu principal");
                                    Console.Clear();
                                    saidaOperacoes = true;
                                    break;

                                default:        //Opção invalida 

                                    Cores.ExibirErro("Opção inválida, digite outra.");
                                    break;

                            }
                          
                        }
                        break;

                    case 7:                     //sair

                        Cores.Continuar("\nFechando o sistema...");
                        return;

                    default:

                        Cores.ExibirErro("Opção inválida, digite outra.");
                        break;

                }
                Cores.Continuar("\nAperte qualquer tecla para continuar.");
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