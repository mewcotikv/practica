#!/bin/bash
ssh-keygen -t rsa -b 4096 -f ~/.ssh/id_rsa -N ""
echo "SSH Key generated successfully!"
echo ""
echo "Copy your public key:"
cat ~/.ssh/id_rsa.pub
