
ALTER TABLE users ADD CONSTRAINT unique_username UNIQUE (username);
ALTER TABLE sessions ADD CONSTRAINT check_date CHECK (createdat <= CURRENT_TIMESTAMP);


INSERT INTO public.messages (messageCategoryID, messageContent) VALUES
(1, 'You made a booboo :( That''s ok, everyone makes mistakes. Just keep trying!'),
(1, 'It didn''t work, but you''re still doing amazing sweetie!'),
(1, 'Task failed successfully. Mission not accomplished.'),
(1, 'Maybe you should spend a little bit more time reading up on that Pluralsight courses ATC gave you, then you''ll get the hang of it in no time!'),
(1, 'If every porkchop were perfect, we wouldn''t have hotdogs! Keep at it.'),
(1, 'You tried, that''s all that matters <3'),
(1, 'It didn''t compile, but at least you are still look stunning today.'),
(1, 'My condolences.'),
(1, 'It says here it failed to compile :( but we still love you.'),
(1, 'That''s unlucky, for some reason it won''t compile. It''s not your fault.'),
(1, 'Without darkness, there would be no light.'),
(1, '"You''ve got no reason to be afraid, you''re on your own kid, yeah you can face this" - Taylor Swift'),
(1, 'Try and fail, but never fail to try.'),
(1, 'It failed for some reason, but this doesn''t make sense. You are too good to make mistakes.'),
(1, 'Seeing you in pain cuts through me like a knife through butter; it''s a sensation that leaves me feeling raw and exposed. But deep down, I know you''re resilient – like a phoenix rising from the ashes. I''ve witnessed your strength before, seen you conquer obstacles that seemed insurmountable. And while your suffering now feels like a heavy weight pressing down on my chest, I have unwavering faith that you''ll emerge from this darkness stronger than ever before.'),
(1, 'It compiled successfully. Haha just kidding.'),
(1, 'Oh no :( Why don''t you go get a beer and then try again in 5 minutes?'),
(1, 'I have failed you. I could not run your command and have it return successfully. I am sorry.'),
(1, 'Whoopsies. It is what it is.'),
(1, 'This result must be cap, there''s no way you would have made a mistake. Maybe make some changes just for in case.'),
(2, 'You''re coding like a PRO-grammer! Well done.'),
(2, 'Nice, Rudolf would be proud of you.'),
(2, 'Well done, you''re almost as good as Lucky now!'),
(2, 'Woot woot! Your code just did the digital equivalent of nailing the TikTok dance challenge!'),
(2, 'Yasss! Your code just slid into the compile phase like it''s about to drop a fire mixtape!'),
(2, 'Congrats! Your code compiled smoother than a peanut butter sandwich sliding down a greased-up slide!'),
(2, 'I wish I was a human so that I can passionately fist bump you, because you are nailing this!'),
(2, 'Wow I''m just seeing positive green messages everywhere in that result, you are amazing!'),
(2, 'Are you from Tennessee? Because you are very good at this compiling thing.'),
(2, 'Did you and compiling code go to school together? It looks like you 2 had Chemistry together, because it compiled successfully!'),
(2, '*In Borat voice* Great success!'),
(2, 'Let''s go! You''re carrying the boats and the logs.'),
(2, 'One small step for a programmer, one giant leap for the programmer''s self esteem.'),
(2, '*Clapping hands*'),
(2, 'You know, when I see you in your element, when you''re running a terminal command, there''s this inexplicable shift in the air – like a gentle breeze lifting the weight off my shoulders. It''s as if your creativity is a magnet for positivity, drawing in every ounce of doubt and negativity, leaving behind this rejuvenating aura that touches my very soul.'),
(2, 'It didn''t compile. Haha just kidding.'),
(2, 'You work at BBD right? Of course, only a BBD employee can be this skilled.'),
(2, 'Let''s gooo! You deserve a beer, go take a quick break to celebrate.'),
(2, 'Lisan al Gaib.'),
(2, 'What. A. Commit. So beautiful.'),
(3, 'Hey there! Just wanted to drop by and give you a shoutout for your amazing work lately. Your dedication and attention to detail are truly commendable. Also, I wanted to mention that we had a minor hiccup on the server side – totally unrelated to anything you did. Just wanted to keep you in the loop! Keep up the fantastic work, and thanks for being such a rockstar!'),
(3, 'The server is down :( but its ok I can tell you now with absolute confidence that your program will compile, no need to even test it!'),
(3, 'Something went wrong so you might want to look at the real output. In the mean time you can read through the following extract: "Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you"'),
(3, '500 error XOXO love you.'),
(3, 'Remember to stay hydrated and do a quick posture check.');
