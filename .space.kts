/**
* JetBrains Space Automation
* This Kotlin-script file lets you automate build activities
* For more info, see https://www.jetbrains.com/help/space/automation.html
*/

job("Build and push Docker") {
    host("Build artifacts and a Docker image") {
      

        dockerBuildPush {
            // Docker context, by default, project root
            // path to Dockerfile relative to project root
            // if 'file' is not specified, Docker will look for it in 'context'/Dockerfile
             file = "Voortrekkers/Dockerfile"
         

            val spaceRepo = "nefdtco.registry.jetbrains.space/p/voortrekkers/docker/voortrekkers"
            // image tags for 'docker push'
            tags {
                +"$spaceRepo:1.0.${"$"}JB_SPACE_EXECUTION_NUMBER"
                +"$spaceRepo:latest"
            }
        }
    }
}