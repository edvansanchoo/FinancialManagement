pipeline {
    agent any
    stages {
        stage('Clean Backend') {
            steps {
                dotnetClean configuration: 'Release', project: '${WORKSPACE}'
            }
        }
        stage('Build Backend') {
            steps {
                dotnetBuild configuration: 'Release', project: '${WORKSPACE}'
            }
        }
        stage('Publish Backend') {
            steps {
                dotnetPublish configuration: 'Release', project: '${WORKSPACE}', selfContained: false
            }
        }
        stage('Copy Files To IIS') {
            steps {
                bat '''@echo off
                        set origem=C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\FinancialManagement\\bin\\Release\\net8.0\\publish\\
                        set destino=C:\\inetpub\\wwwroot\\FinancialManagement

                        echo Copiando arquivos de %origem% para %destino% ...

                        xcopy /s /y %origem% %destino%

                        echo Arquivos copiados com sucesso!
                        pause'''
            }
        }
    }
}