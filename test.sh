docker build -q -t lda --build-arg CONFIGURATION=Debug .
docker run --rm --name lda -d -p 8080:8080 -e READ_MEMORY_API=http://localhost:8080/api/v1/readMemory lda

sleep 5

RESULT=`curl -s --header "Content-Type: application/json" \
  --request POST \
  --data '{"opcode":0,"state":{"a":242,"b":0,"c":0,"d":5,"e":15,"h":10,"l":20,"flags":{"sign":true,"zero":false,"auxCarry":false,"parity":false,"carry":false},"programCounter":1,"stackPointer":2,"cycles":0}}' \
  http://localhost:8080/api/v1/execute\?highByte=5\&lowByte=8`
EXPECTED='{"opcode":0,"state":{"a":10,"b":0,"c":0,"d":5,"e":15,"h":10,"l":20,"flags":{"sign":true,"zero":false,"auxCarry":false,"parity":false,"carry":false},"programCounter":1,"stackPointer":2,"cycles":13}}'

docker kill lda

DIFF=`diff <(jq -S . <<< "$RESULT") <(jq -S . <<< "$EXPECTED")`

if [ $? -eq 0 ]; then
    echo -e "\e[32mLDA Test Pass \e[0m"
    exit 0
else
    echo -e "\e[31mLDA Test Fail  \e[0m"
    echo "$RESULT"
    echo "$DIFF"
    exit -1
fi