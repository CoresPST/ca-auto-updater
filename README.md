# auto-updater

Aqui tem partes do sistema de atualização do Counter Advanced, separadas pra ficar fácil de ler. A ideia é conferir antes de trocar, evitar duas instalações ao mesmo tempo e não sobrescrever arquivo pela metade.

| arquivo | o que faz |
| --- | --- |
| `src/FileHash.cs` | SHA-256 lendo por stream |
| `src/UpdatePlan.cs` | exemplo reduzido que encontra arquivos ausentes ou diferentes |
| `src/Paths.cs` | limita os destinos à pasta da instalação |
| `src/AtomicFiles.cs` | cópia e escrita com temporário |
| `src/UpdateLock.cs` | cria um mutex diferente pra cada instalação |

Rode `powershell -File build.ps1` no Windows pra gerar `bin/Portfolio.dll`. Os helpers vieram do projeto; o `UpdatePlan` foi simplificado pra demonstrar o fluxo sem depender do manifesto e do servidor reais.

Isso é uma biblioteca de exemplos, não um atualizador pronto. O plano deve receber uma lista já autenticada. No jogo existe validação da assinatura do manifesto antes de confiar nesses dados; SHA-256 sozinho não prova quem publicou o arquivo.

[Lógica e otimização](COMO-FUNCIONA.md)

Pra rodar os testes: `powershell -File test.ps1`.
