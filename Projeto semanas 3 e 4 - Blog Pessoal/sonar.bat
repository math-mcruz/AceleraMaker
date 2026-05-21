@echo off
set PROJECT_KEY=blog-pessoal-dotnet
set URL=http://localhost:9000

echo [1/4] begin
dotnet sonarscanner begin /k:"%PROJECT_KEY%" /d:sonar.host.url="%URL%" /d:sonar.login="%SONAR_TOKEN%" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/**,**/Config/**,**/Models/**,**/DTOs/**,Program.cs"

echo [2/4] clean, build
dotnet clean BlogPessoal/BlogPessoal.slnx
dotnet build BlogPessoal/BlogPessoal.slnx

echo [3/4] test
dotnet test BlogPessoal/BlogPessoal.slnx /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

echo [4/4] end
dotnet sonarscanner end /d:sonar.login="%SONAR_TOKEN%"

pause