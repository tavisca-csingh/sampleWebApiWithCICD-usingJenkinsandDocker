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
                bat'''	dotnet build  %SOLUTION_FILE_PATH -p:Configuration=release -v:q
			dotnet test %TEST_PROJECT_PATH%
			dotnet publish WebApi -c Release -o ../artifacts
		    '''
            }
        }
	    
       stage('docker Image creation')
        {    
		steps
		{
                bat'''
		docker build --tag=%DOCKER_HUB_USERNAME%/%DOCKER_IMAGE_NAME%:%DOCKER_IMAGE_TAG% --build-arg project_name=%APPLICATION_NAME% .
		'''
                }
        }
        
        stage('Push DockerImage') 
        {
            steps {
		    bat'''
			docker login -u  %DOCKER_HUB_USERNAME% -p %DOCKER_PASSWORD% 
			docker push %DOCKER_HUB_USERNAME%/%DOCKER_IMAGE_NAME%:%DOCKER_IMAGE_TAG%
			'''                        
	   	  }
		
        }
	   


    }
	
}

