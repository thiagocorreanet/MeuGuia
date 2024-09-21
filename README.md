# Meu Guia - Gestão Financeira 💰

Meu Guia é um projeto dedicado a armazenar dados sobre a gestão financeira pessoal. A ideia é evoluir além das planilhas tradicionais, transformando-as em uma ferramenta simples e fácil de usar.
Para este projeto, utilizaremos como exemplo uma planilha do canal Primo Pobre, que considero muito interessante por sua clareza e objetividade.

![image](https://github.com/user-attachments/assets/d470fb49-ed3c-4678-a14c-2f8f05ae7314)

Receitas: Teremos uma área dedicada ao cadastro de nossas receitas, seja proveniente de salários ou de trabalhos extras que você esteja realizando.

Nessa área, teremos duas informações muito importantes para armazenar em nosso projeto: o valor que irá sobrar naquele mês e um cenário de emergência. Para o cenário de emergência, vamos pegar o total das vendas e multiplicar por 6 (seis meses).

![image](https://github.com/user-attachments/assets/10064b4c-5c41-4249-8541-8894ac7958e3)

**Despesas:** Nesta área, teremos dois tipos de despesas: essenciais e não essenciais, além do total das suas despesas.

![image](https://github.com/user-attachments/assets/4ef608e8-9dc2-4fc7-ae89-c9e6b992ac76)

# 🗂️ Estrutura do projeto

👉 **WebAPI:** Projeto de API construida utilizando .NET8 com uma documentaão rica do swagger.
👉 **Application:** Projeto que consiste em absorver toda a lógica do sistema.
      - **Herlper:** Pasta para armazenar métodos uteis para o desenvolvimento.
      - **CQRS:** Neste projeto utilizaremos o padrão CQRS para separar em nosso sistema cada responsabilidade.
      - **Mapeamento:** Iremos utilizar AutoMapper para abstrair nossos mapeamento entre classes.
      - **Notification:** Estrutura de notificação da api para nossos usuários.
      



