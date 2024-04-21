& '.\Docker Desktop Installer.exe'  install --installation-dir=D:\Docker --wsl-default-data-root=D:\Docker\WSL --windows-containers-default-data-root=D:\\Docker\\win

#Installation

Download and run the installer for the latest release.
Or if using PowerShell, use this command:

`New-Item -Path 'c:\' -Name 'minikube' -ItemType Directory -Force
Invoke-WebRequest -OutFile 'c:\minikube\minikube.exe' -Uri 'https://github.com/kubernetes/minikube/releases/latest/download/minikube-windows-amd64.exe' -UseBasicParsing`


Add the minikube.exe binary to your PATH.
Make sure to run PowerShell as Administrator.

`$oldPath = [Environment]::GetEnvironmentVariable('Path', [EnvironmentVariableTarget]::Machine)
if ($oldPath.Split(';') -inotcontains 'C:\minikube'){
  [Environment]::SetEnvironmentVariable('Path', $('{0};C:\minikube' -f $oldPath), [EnvironmentVariableTarget]::Machine)
}
`

#Set docker context
`docker context show`
`docker context use default`

#Set default driver for minikube
`minikube config set driver docker`




#Minikube

#Manage your cluster

#Start 
`minikube start --driver=docker`

#Pause Kubernetes without impacting deployed applications:
`minikube pause`

#Unpause a paused instance:
`minikube unpause`

#Halt the cluster:
`minikube stop`

#Change the default memory limit (requires a restart):
`minikube config set memory 9001`

#Browse the catalog of easily installed Kubernetes services:
`minikube addons list`

#Create a second cluster running an older Kubernetes release:
`minikube start -p aged --kubernetes-version=v1.16.1`

#Delete all of the minikube clusters:
`minikube delete --all`


Show pods
`kubectl get pod`
`kubectl get pod -o wide`

Show nodes
`kubectl get nodes`

Show pods
`kubectl get node`

Show services
`kubectl get services`

Create a deployment
`kubectl create deployment demo1 --image=thanhnmitc/webapi1`

Edit a deployment
`kubectl edit deployment demo1`

Show deployments
`kubectl get deployment`

Get Deployment info from etsd
`kubectl get deployment [deployment-name] -o yaml > [filename.yaml]`

Show replicaset
`kubectl get replicaset`

Debug pod
`kubectl logs [pod-name]`

Open bash in pod
`kubectl exec -it demo1-7c8d648c8f-jqw9w -- bash`

Exit bash in pod
`exit`

Delete deployment
`kubectl delete deployment [deployment name]`

Apply config file for 1 deployment
`kubectl apply -f [filename.yaml] [-n [namespace]]`

Create secret from pfx
`kubectl create secret generic aspnetapppfx --from-file=pfx-cert=./certificates/aspnetapp.pfx`

Create configMap from pfx
`kubectl create configmap aspnetappcer --from-file=cert.pfx=./certificates/aspnetapp.pfx`

Run load balancer service
`G:\MiniKube\minikube.exe service webapp1-service`

View namespace
`kubectl get namespace`

View cluster info
`kubectl cluster-info`

View sources applied/not apply namespace
`kubectl api-resources --namespace=false`
`kubectl api-resources --namespace=true`

Get resource in a namespace
`kubvectl get all -n namespace`

Install kubectx & kubens
`winget install --id ahmetb.kubectx`
`winget install --id ahmetb.kubens`

Create namespace
`kubectl create namespace [Name]`

Enable ingress in Minikube
`minikube addons enable ingress`

Describe ingress
`kubectl describe ingress webapi1-ingress`

#HELM CHART 

Get latest charts
`helm repo update`

Install a chart
`helm install [chart-name] [options]`
helm install bitnami/mysql --generate-name

`helm install [release-name] [chart-name] [options]`

View charts are deployed
`helm list`

Uninstall chart
`helm uninstalled [name-release]`

Check release status
`helm status [release name]`

Show values in charty
`helm show values [chartname]`

Create new value.yml file
`echo '[values]' > values.yml`
echo '{mariadb.auth.database: user0db, mariadb.auth.username: user0}' > values.yaml

Install Chart  with values.yml
`helm install -f values.yml [chart name]`


Chart Management
helm create <name>                      # Creates a chart directory along with the common files and directories used in a chart.
helm package <chart-path>               # Packages a chart into a versioned chart archive file.
helm lint <chart>                       # Run tests to examine a chart and identify possible issues:
helm show all <chart>                   # Inspect a chart and list its contents:
helm show values <chart>                # Displays the contents of the values.yaml file
helm pull <chart>                       # Download/pull chart 
helm pull <chart> --untar=true          # If set to true, will untar the chart after downloading it
helm pull <chart> --verify              # Verify the package before using it
helm pull <chart> --version <number>    # Default-latest is used, specify a version constraint for the chart version to use
helm dependency list <chart>            # Display a list of a chart’s dependencies:

Install and Uninstall Apps
helm install <name> <chart>                           # Install the chart with a name
helm install <name> <chart> --namespace <namespace>   # Install the chart in a specific namespace
helm install <name> <chart> --set key1=val1,key2=val2 # Set values on the command line (can specify multiple or separate values with commas)
helm install <name> <chart> --values <yaml-file/url>  # Install the chart with your specified values
helm install <name> <chart> --dry-run --debug         # Run a test installation to validate chart (p)
helm install <name> <chart> --verify                  # Verify the package before using it 
helm install <name> <chart> --dependency-update       # update dependencies if they are missing before installing the chart
helm uninstall <name>                                 # Uninstall a release
Perform App Upgrade and Rollback
helm upgrade <release> <chart>                            # Upgrade a release
helm upgrade <release> <chart> --atomic                   # If set, upgrade process rolls back changes made in case of failed upgrade.
helm upgrade <release> <chart> --dependency-update        # update dependencies if they are missing before installing the chart
helm upgrade <release> <chart> --version <version_number> # specify a version constraint for the chart version to use
helm upgrade <release> <chart> --values                   # specify values in a YAML file or a URL (can specify multiple)
helm upgrade <release> <chart> --set key1=val1,key2=val2  # Set values on the command line (can specify multiple or separate valuese)
helm upgrade <release> <chart> --force                    # Force resource updates through a replacement strategy
helm rollback <release> <revision>                        # Roll back a release to a specific revision
helm rollback <release> <revision>  --cleanup-on-fail     # Allow deletion of new resources created in this rollback when rollback fails
List, Add, Remove, and Update Repositories
helm repo add <repo-name> <url>   # Add a repository from the internet:
helm repo list                    # List added chart repositories
helm repo update                  # Update information of available charts locally from chart repositories
helm repo remove <repo_name>      # Remove one or more chart repositories
helm repo index <DIR>             # Read the current directory and generate an index file based on the charts found.
helm repo index <DIR> --merge     # Merge the generated index with an existing index file
helm search repo <keyword>        # Search repositories for a keyword in charts
helm search hub <keyword>         # Search for charts in the Artifact Hub or your own hub instance
Helm Release monitoring
helm list                       # Lists all of the releases for a specified namespace, uses current namespace context if namespace not specified
helm list --all                 # Show all releases without any filter applied, can use -a
helm list --all-namespaces      # List releases across all namespaces, we can use -A
helm -l key1=value1,key2=value2 # Selector (label query) to filter on, supports '=', '==', and '!='
helm list --date                # Sort by release date
helm list --deployed            # Show deployed releases. If no other is specified, this will be automatically enabled
helm list --pending             # Show pending releases
helm list --failed              # Show failed releases
helm list --uninstalled         # Show uninstalled releases (if 'helm uninstall --keep-history' was used)
helm list --superseded          # Show superseded releases
helm list -o yaml               # Prints the output in the specified format. Allowed values: table, json, yaml (default table)
helm status <release>           # This command shows the status of a named release.
helm status <release> --revision <number>   # if set, display the status of the named release with revision
helm history <release>          # Historical revisions for a given release.
helm env                        # Env prints out all the environment information in use by Helm.
Download Release Information
helm get all <release>      # A human readable collection of information about the notes, hooks, supplied values, and generated manifest file of the given release.
helm get hooks <release>    # This command downloads hooks for a given release. Hooks are formatted in YAML and separated by the YAML '---\n' separator.
helm get manifest <release> # A manifest is a YAML-encoded representation of the Kubernetes resources that were generated from this release's chart(s). If a chart is dependent on other charts, those resources will also be included in the manifest.
helm get notes <release>    # Shows notes provided by the chart of a named release.
helm get values <release>   # Downloads a values file for a given release. use -o to format output
Plugin Management
helm plugin install <path/url1>     # Install plugins
helm plugin list                    # View a list of all installed plugins
helm plugin update <plugin>         # Update plugins
helm plugin uninstall <plugin>      # Uninstall a plugin


https://github.com/helmfile/helmfile/releases

helm plugin install https://github.com/aslafy-z/helm-git --version 0.16.0