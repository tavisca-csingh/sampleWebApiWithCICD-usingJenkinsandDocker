pipeline {
    agent any
    parameters {
        string(defaultValue: 'https://github.com/tavisca-csingh/sampleWebApiForJenkins-Practice.git', name: 'GIT_SSH_PATH')
        string(defaultValue: 'WebApi.sln', name: 'SOLUTION_FILE_PATH')
        string(defaultValue: 'WebApi.Tests/WebApi.Tests.csproj', name: 'TEST_PROJECT_PATH')
	string(name: 'APPLICATION_NAME', defaultValue: 'WebApi')
	string(name: 'DOCKER_HUB_USERNAME', defaultValue: 'ichandan8')
        string(name: 'DOCKER_HUB_CREDENTIALS_ID', defaultValue: 'DockerCredentials')
        string(name: 'DOCKER_IMAGE_NAME', defaultValue: 'demo-image')
        string(name: 'DOCKER_IMAGE_TAG', defaultValue: 'latest')
    }
    stages {
        stage('Build') {
            steps {
                powershell '''
			dotnet restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json
			dotnet build  ${SOLUTION_FILE_PATH} -p:Configuration=release -v:q
			
			dotnet publish WebApi -c Release -o artifacts
			dotnet test ${TEST_PROJECT_PATH}
		'''
            }
        }
	    
       stage('Set-up for docker CustomImage creation')
        {
            steps
            {
                powershell "mv Dockerfile ${env.APPLICATION_NAME}/artifacts"
            }
        }
        stage('Build Custom Docker Image')
        {
            steps 
            {
                script 
                {
                    dir("${env.APPLICATION_NAME}/artifacts") 
                    {
                       
                        powershell "docker build -t ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} --build-arg APPLICATION_NAME_TO_BE_HOSTED=${env.APPLICATION_NAME} ."
                    }
                }
            }
        }
        stage('Push Docker CustomImage to DockerIO registry') 
        {
            steps {
                script {
                    // CustomImage.push()
                    docker.withRegistry('https://www.docker.io/', "${env.DOCKER_HUB_CREDENTIALS_ID}") 
                    {
                        powershell "docker push ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"   
                    }
                }
            }
        }


    }
	
}
