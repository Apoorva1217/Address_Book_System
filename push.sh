read a
git branch $a
git checkout $a
git add .
git commit -m "[Apoorva] Add . Ability to Delete entry from AddressBook JSON Server"
git push origin $a
git checkout master
git merge $a
git push origin master --force
