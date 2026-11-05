# lógica da atualização

## decidir o que precisa baixar

Primeiro confiro se o arquivo existe e se o tamanho bate. Se já estiver diferente, não gasto uma leitura inteira calculando hash. Quando o tamanho bate, comparo o SHA-256. O exemplo devolve os caminhos que precisam de troca.

O hash usa stream, então não coloca um pacote de vários GB inteiro na memória. O plano aceita cancelamento entre arquivos; o cálculo individual do hash ainda termina antes de perceber o cancelamento. Não vou chamar isso de cancelamento instantâneo.

## uma instalação por vez

O helper de mutex usa o caminho da instalação pra gerar o nome do bloqueio. Duas pastas diferentes podem atualizar separadas. Quem chama precisa adquirir o mutex, tratar abandono e liberar no final. Criar o objeto por si só não pega o bloqueio.

## não sair escrevendo em qualquer pasta

O caminho relativo é combinado com a raiz e normalizado. Se escapar da pasta ou atravessar um link detectado, é recusado. Essa checagem é uma camada de defesa; não elimina sozinha uma corrida com outro processo alterando diretórios.

## instalar sem deixar metade

O arquivo novo é escrito num temporário e só depois ocupa o lugar do antigo. No sistema completo, uma atualização com vários arquivos também precisa de um diário e recuperação. O helper de um arquivo não faz uma transação de toda a instalação.

## como pensar em otimização

Evitar leitura repetida costuma ajudar mais que encher de threads em um HD. Primeiro dá pra medir tempo de busca, hash, download e cópia separados. Depois fica claro onde está o custo.

Também precisa mostrar progresso: uma verificação longa com a mensagem parada parece travamento. Não publiquei aqui tokens, endereço de serviço ou manifestos da instalação real.
