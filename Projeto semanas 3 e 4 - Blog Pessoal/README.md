
# Ajustes (appsettings.json e sonar.bat(na pasta junto com README.md))

## Ajuste na senha do Banco de Dados(forma sugerida)
* Pesquise no Windows por "Editar as variáveis de ambiente do sistema".
* Em "Variáveis de usuário", clique em "Novo" e preencha:
  * **Nome:** `SENHA_BANCO_LOCAL`
  * **Valor:** `A senha do seu banco de dados.`
## Ajuste no Token do Gemini API
  * **Nome:** `GEMINI_KEY`
  * **Valor:** `Seu Token de acesso do Gemini.`
  ## Ajuste na Chave Secreta
  * **Nome:** `SECRET_KEY`
  * **Valor:** `Sua chave secrata para o token JWT.`
  ## Ajuste no Token do script de automação (sonar.bat)
  * **Nome:** `SONAR_TOKEN`
  * **Valor:** `Seu Token de acesso do SonarQube.`
* Reinicie o Visual Studio (ou terminal) para aplicar a mudança.