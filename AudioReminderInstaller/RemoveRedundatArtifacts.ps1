$Artifacts = $args[0]

# cd $Artifacts

Write-Output "Removing redundant artifacts"
# delete log and pdb files from articats dir
remove-item -path $Artifacts -include ("*.pdb","*.log") -Recurse
exit