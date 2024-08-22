CREATE DATABASE IF NOT EXISTS vendas_gestao;
USE vendas_gestao;

CREATE TABLE funcionario (
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(55),
    cpf VARCHAR(16)
);

CREATE TABLE dispositivo(
    id_dispositivo INT PRIMARY KEY AUTO_INCREMENT,
    alicota VARCHAR(10),
    descricao LONGTEXT,
    status_dispo VARCHAR(200)
);

CREATE TABLE caixa(
    id_caixa INT PRIMARY KEY AUTO_INCREMENT,
    saldo_inicial DOUBLE,
    total_entradas DOUBLE,
    total_saidas DOUBLE,
    saldo_final DOUBLE,
    status_caixa VARCHAR(255),
    fk_funcionario INT NOT NULL,
    FOREIGN KEY (fk_funcionario) REFERENCES funcionario(id_funcionario)
);

CREATE TABLE despesa(
    id_despensa INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    data_vencimento DATE,
    data_pagamento DATE,
    status_despensa VARCHAR(255),
    fk_caixa INT,
    FOREIGN KEY (fk_caixa) REFERENCES caixa(id_caixa)
);

CREATE TABLE cliente(
    id_cliente INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(55),
    cpf VARCHAR(16),
    email VARCHAR(55),
    telefone VARCHAR(23),
    data_nascimento DATE
);

CREATE TABLE servico (
    id_servico INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    descricao LONGTEXT,
    tempo TIME
);

CREATE TABLE venda (
    id_venda INT PRIMARY KEY AUTO_INCREMENT,
    data_venda DATE,
    hora TIME,
    valor_total DOUBLE,
    desconto DOUBLE,
    tipo VARCHAR(200),
    fk_cliente INT,
    FOREIGN KEY (fk_cliente) REFERENCES cliente(id_cliente)
);

CREATE TABLE venda_servico (
    id_venda_servico INT PRIMARY KEY AUTO_INCREMENT,
    fk_venda INT,
    fk_servico INT,
    valor_unitario DOUBLE,
    quantidade INT,
    FOREIGN KEY (fk_venda) REFERENCES venda(id_venda) ON DELETE CASCADE,
    FOREIGN KEY (fk_servico) REFERENCES servico(id_servico)
);

CREATE TABLE recebimento (
    id_recebimento INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    data_vencimento DATE,
    data_pagamento DATE,
    status_recebimento VARCHAR(55),
    fk_caixa INT,
    fk_venda INT,
    FOREIGN KEY (fk_venda) REFERENCES venda(id_venda),
    FOREIGN KEY (fk_caixa) REFERENCES caixa(id_caixa)
);

CREATE TABLE encargo (
    id_encargo INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    descricao LONGTEXT,
    fk_recebimento INT,
    FOREIGN KEY (fk_recebimento) REFERENCES recebimento(id_recebimento)
);

insert into cliente (nome, cpf, email, telefone, data_nascimento) values ('Theo Theodoro Novais', '02920920909', 'theo.tn@gmail.com', '920940928', '2005-10-21');
insert into cliente (nome, cpf, email, telefone, data_nascimento) values ('Gabriel de Oliveira Silva', '2092423523', 'gabriel.dos@gmail.com', '920940928', '2004-09-26');
insert into funcionario(nome,cpf) values ('Joaquin Buckley','02202464670');
select * from funcionario;
