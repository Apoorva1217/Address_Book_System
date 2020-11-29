read a
git branch $a
git checkout $a
git add .
git commit -m "[Apoorva] Add . Add Contact to Address Book with Thread with Synchronization"
git push origin $a
git checkout master
git merge $a
git push origin master --force
