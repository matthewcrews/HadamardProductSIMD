
;HadamardProduct.hadamardProduct(System.Span`1<Int32>, System.Span`1<Double>, System.Span`1<Int32>, System.Span`1<Double>)
L0000: push r15
L0002: push r14
L0004: push r13
L0006: push r12
L0008: push rdi
L0009: push rsi
L000a: push rbp
L000b: push rbx
L000c: sub rsp, 0x48
L0010: vzeroupper
L0013: mov rsi, [r9]
L0016: mov edi, [r9+8]
L001a: mov rbx, [r8]
L001d: mov ebp, [r8+8]
L0021: mov r14, [rdx]
L0024: mov r15d, [rdx+8]
L0028: mov r12, [rcx]
L002b: mov r13d, [rcx+8]
L002f: mov ecx, r13d
L0032: mov eax, ebp
L0034: cmp ecx, eax
L0036: jle short L003c
L0038: mov eax, ebp
L003a: jmp short L003f
L003c: mov eax, r13d
L003f: mov [rsp+0x34], eax
L0043: mov ecx, eax
L0045: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.Int32, System.Private.CoreLib]](Int32)
L004a: mov [rsp+0x28], rax
L004f: mov ecx, [rsp+0x34]
L0053: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.Double, System.Private.CoreLib]](Int32)
L0058: xor ecx, ecx
L005a: xor edx, edx
L005c: xor r8d, r8d
L005f: mov r9d, ebp
L0062: sar r9d, 0x1f
L0066: and r9d, 3
L006a: add r9d, ebp
L006d: and r9d, 0xfffffffc
L0071: mov r10d, ebp
L0074: sub r10d, r9d
L0077: mov r9d, ebp
L007a: sub r9d, r10d
L007d: xor r10d, r10d
L0080: test ebp, ebp
L0082: je short L0091
L0084: mov [rsp+0x20], rbx
L0089: mov r10, rbx
L008c: mov rbx, [rsp+0x20]
L0091: cmp ecx, r13d
L0094: jge L01bb
L009a: mov [rsp+0x44], r9d
L009f: cmp edx, r9d
L00a2: setl r11b
L00a6: movzx r11d, r11b
L00aa: test r11d, r11d
L00ad: je L01bb
L00b3: movsxd r11, ecx
L00b6: lea r11, [r12+r11*4]
L00ba: mov r11d, [r11]
L00bd: vmovd xmm0, r11d
L00c2: vpbroadcastd xmm0, xmm0
L00c7: movsxd r9, edx
L00ca: mov [rsp+0x38], r10
L00cf: vmovdqu xmm1, [r10+r9*4]
L00d5: vpcmpeqd xmm0, xmm0, xmm1
L00d9: vpmovmskb r9d, xmm0
L00dd: test r9d, r9d
L00e0: jle L0178
L00e6: mov [rsp+0x20], rbx
L00eb: tzcnt r9d, r9d
L00f0: mov r10d, r9d
L00f3: sar r10d, 0x1f
L00f7: and r10d, 3
L00fb: add r9d, r10d
L00fe: sar r9d, 2
L0102: mov r10, [rsp+0x28]
L0107: cmp r8d, [r10+8]
L010b: jae L0319
L0111: movsxd rbx, r8d
L0114: mov [r10+rbx*4+0x10], r11d
L0119: cmp ecx, r15d
L011c: jae L0319
L0122: movsxd r11, ecx
L0125: lea r11, [r14+r11*8]
L0129: vmovsd xmm0, [r11]
L012e: add r9d, edx
L0131: cmp r9d, edi
L0134: jae L0319
L013a: movsxd r11, r9d
L013d: lea r11, [rsi+r11*8]
L0141: vmulsd xmm0, xmm0, [r11]
L0146: cmp r8d, [rax+8]
L014a: jae L0319
L0150: movsxd r11, r8d
L0153: vmovsd [rax+r11*8+0x10], xmm0
L015a: inc ecx
L015c: inc r8d
L015f: mov [rsp+0x28], r10
L0164: mov rbx, [rsp+0x20]
L0169: mov r9d, [rsp+0x44]
L016e: mov r10, [rsp+0x38]
L0173: jmp L0091
L0178: mov r9d, r11d
L017b: lea r11d, [rdx+3]
L017f: cmp r11d, ebp
L0182: jae L0319
L0188: lea r11d, [rdx+3]
L018c: movsxd r11, r11d
L018f: lea r11, [rbx+r11*4]
L0193: cmp r9d, [r11]
L0196: jle short L01aa
L0198: add edx, 4
L019b: mov r9d, [rsp+0x44]
L01a0: mov r10, [rsp+0x38]
L01a5: jmp L0091
L01aa: inc ecx
L01ac: mov r9d, [rsp+0x44]
L01b1: mov r10, [rsp+0x38]
L01b6: jmp L0091
L01bb: cmp ecx, r13d
L01be: jge L0273
L01c4: cmp edx, ebp
L01c6: setl r11b
L01ca: movzx r11d, r11b
L01ce: test r11d, r11d
L01d1: je L0273
L01d7: movsxd r9, ecx
L01da: lea r11, [r12+r9*4]
L01de: mov r9d, [r11]
L01e1: mov r10d, r9d
L01e4: cmp edx, ebp
L01e6: jae L0319
L01ec: movsxd r11, edx
L01ef: lea r11, [rbx+r11*4]
L01f3: mov r11d, [r11]
L01f6: cmp r10d, r11d
L01f9: jne short L0260
L01fb: mov r10, [rsp+0x28]
L0200: cmp r8d, [r10+8]
L0204: jae L0319
L020a: movsxd r11, r8d
L020d: mov [r10+r11*4+0x10], r9d
L0212: cmp ecx, r15d
L0215: jae L0319
L021b: movsxd r9, ecx
L021e: lea r11, [r14+r9*8]
L0222: vmovsd xmm0, [r11]
L0227: cmp edx, edi
L0229: jae L0319
L022f: movsxd r11, edx
L0232: lea r11, [rsi+r11*8]
L0236: vmulsd xmm0, xmm0, [r11]
L023b: cmp r8d, [rax+8]
L023f: jae L0319
L0245: movsxd r9, r8d
L0248: vmovsd [rax+r9*8+0x10], xmm0
L024f: inc r8d
L0252: inc ecx
L0254: inc edx
L0256: mov [rsp+0x28], r10
L025b: jmp L01bb
L0260: cmp r9d, r11d
L0263: jge short L026c
L0265: inc ecx
L0267: jmp L01bb
L026c: inc edx
L026e: jmp L01bb
L0273: mov r10, [rsp+0x28]
L0278: test r10, r10
L027b: jne short L028c
L027d: test r8d, r8d
L0280: jne L0313
L0286: xor esi, esi
L0288: xor edi, edi
L028a: jmp short L029e
L028c: mov ecx, [r10+8]
L0290: mov edx, r8d
L0293: cmp rcx, rdx
L0296: jb short L0313
L0298: mov rsi, r10
L029b: mov edi, r8d
L029e: test rax, rax
L02a1: jne short L02ae
L02a3: test r8d, r8d
L02a6: jne short L0313
L02a8: xor ebx, ebx
L02aa: xor ebp, ebp
L02ac: jmp short L02bf
L02ae: mov ecx, [rax+8]
L02b1: mov edx, r8d
L02b4: cmp rcx, rdx
L02b7: jb short L0313
L02b9: mov rbx, rax
L02bc: mov ebp, r8d
L02bf: mov rcx, 0x7ffb7aa2f848
L02c9: call 0x00007ffbd871a470
L02ce: mov r14, rax
L02d1: mov rdx, rsi
L02d4: lea rsi, [r14+8]
L02d8: mov rcx, rsi
L02db: call 0x00007ffbd871a050
L02e0: xor edx, edx
L02e2: mov [rsi+8], edx
L02e5: mov [rsi+0xc], edi
L02e8: lea rsi, [r14+0x18]
L02ec: mov rcx, rsi
L02ef: mov rdx, rbx
L02f2: call 0x00007ffbd871a050
L02f7: xor eax, eax
L02f9: mov [rsi+8], eax
L02fc: mov [rsi+0xc], ebp
L02ff: mov rax, r14
L0302: add rsp, 0x48
L0306: pop rbx
L0307: pop rbp
L0308: pop rsi
L0309: pop rdi
L030a: pop r12
L030c: pop r13
L030e: pop r14
L0310: pop r15
L0312: ret
L0313: call System.ThrowHelper.ThrowArgumentOutOfRangeException()
L0318: int3
L0319: call 0x00007ffbd885a370
L031e: int3
