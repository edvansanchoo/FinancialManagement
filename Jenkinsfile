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
    }
}