pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Build .NET') {
            steps {
                sh 'dotnet build ecommerce.sln'
            }
        }
        stage('Docker Build') {
            steps {
                sh 'docker build -t ecommerce-app:latest .'
            }
        }
        stage('Deploy') {
            steps {
                sh '''
                    docker stop ecommerce-app || true
                    docker rm ecommerce-app || true
                    docker run -d -p 80:80 --name ecommerce-app ecommerce-app:latest
                '''
            }
        }
    }
}