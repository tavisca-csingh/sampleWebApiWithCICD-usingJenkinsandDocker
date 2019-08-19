pipeline {
    agent any
    parameters {
        string(defaultValue: 'https://github.com/tavisca-csingh/sampleWebApiWithCICD-usingJenkinsandDocker', name: 'GIT_SSH_PATH')
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
                powershell 
		    '''
			dotnet restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json
			dotnet build  ${SOLUTION_FILE_PATH} -p:Configuration=release -v:q
			dotnet test ${TEST_PROJECT_PATH}
			dotnet publish WebApi -c Release -o artifacts
			
		    '''
            }
        }
	    
       stage('docker Image creation')
        {
            steps
            {
                powershell 
		'''
		mv Dockerfile ${env.APPLICATION_NAME}/artifacts
		docker build -t ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} --build-arg ${env.APPLICATION_NAME} .
		'''
            }
        }
        
        stage('Push DockerImage') 
        {
            steps {
		    script {
                    docker.withRegistry('https://www.docker.io/', "${env.DOCKER_HUB_CREDENTIALS_ID}") 
                    {
                        powershell "docker push ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"   
                    }
                }
            }
        }


    }
	
}
