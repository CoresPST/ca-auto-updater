# o auto-updater

essa é a ideia do atualizador que fui usando no Counter Advanced, explicado por cima mesmo.

o launcher olha qual versão ta instalada e consulta a versão publicada. junto vem a informação do pacote que pertence àquela atualização, ai ele consegue saber o que baixar.

os downloads ficam no GitHub Releases. o launcher mostra o andamento e confere o pacote antes de começar a trocar os arquivos. só terminar de baixar não quer dizer que já instalou.

outra coisa que ajuda é o pacote cumulativo. se a pessoa ficou umas versões sem atualizar, ela não precisa ir instalando uma por uma pra chegar na atual.

a parte chata é quando o próprio launcher precisa atualizar kkk. ele não consegue sair substituindo o arquivo que ta usando. então tem uma etapa que espera ele fechar, aplica a troca e abre de novo.

se acontecer algum problema no caminho, não da pra salvar a versão nova como se tivesse dado tudo certo. fui tratando isso junto com cancelamento, reparo e atualização interrompida.

tbm deixei as configurações pessoais fora dessa troca. a atualização é pros arquivos do jogo e do launcher, não pra zerar as escolhas de quem joga.

resumindo o caminho: conferir a versão, baixar, conferir o pacote, aplicar e só depois registrar que terminou.

[a base do launcher ta aqui](https://github.com/CoresPST/ca-launcher).
